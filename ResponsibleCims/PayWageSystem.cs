// Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Game.Simulation.PayWageSystem
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using Game;
using Game.Agents;
using Game.Citizens;
using Game.Common;
using Game.Companies;
using Game.Economy;
using Game.Prefabs;
using Game.Simulation;
using Game.Tools;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;

namespace ResponsibleCims
{
    [CompilerGenerated]
    public class PayWageSystem : GameSystemBase
    {
        private struct Payment
        {
            public Entity m_Target;

            public int m_Amount;
        }

        [BurstCompile]
        private struct PayJob : IJob
        {
            public NativeQueue<Payment> m_PaymentQueue;

            public BufferLookup<Game.Economy.Resources> m_Resources;

            public void Execute()
            {
                Payment item;
                while (m_PaymentQueue.TryDequeue(out item))
                {
                    if (m_Resources.HasBuffer(item.m_Target))
                    {
                        EconomyUtils.AddResources(Resource.Money, item.m_Amount, m_Resources[item.m_Target]);
                    }
                }
            }
        }

        [BurstCompile]
        private struct PayWageJob : IJobChunk
        {
            [ReadOnly]
            public EntityTypeHandle m_EntityType;

            public ComponentTypeHandle<TaxPayer> m_TaxPayerType;

            [ReadOnly]
            public BufferTypeHandle<HouseholdCitizen> m_HouseholdCitizenType;

            public BufferTypeHandle<Game.Economy.Resources> m_ResourcesType;

            [ReadOnly]
            public SharedComponentTypeHandle<UpdateFrame> m_UpdateFrameType;

            [ReadOnly]
            public ComponentTypeHandle<CommuterHousehold> m_CommuterHouseholdType;

            [ReadOnly]
            public ComponentLookup<CompanyData> m_Companies;

            [ReadOnly]
            public ComponentLookup<Citizen> m_Citizens;

            [ReadOnly]
            public ComponentLookup<Worker> m_Workers;

            [ReadOnly]
            public ComponentLookup<Student> m_Students;

            [ReadOnly]
            public NativeArray<int> m_TaxRates;

            public EconomyParameterData m_EconomyParameters;

            public uint m_UpdateFrameIndex;

            public NativeQueue<Payment>.ParallelWriter m_PaymentQueue;

            private void PayWage(Entity workplace, Entity worker, Entity household, Worker workerData, ref TaxPayer taxPayer, DynamicBuffer<Game.Economy.Resources> resources, CitizenAge age, bool isCommuter, bool isStudent, ref EconomyParameterData economyParameters)
            {
                int num = 0;
                int num2;

                if (!(workplace != Entity.Null))
                {
                    num2 = age switch
                    {
                        CitizenAge.Child => economyParameters.m_FamilyAllowance / kUpdatesPerDay,
                        CitizenAge.Elderly => economyParameters.m_Pension / kUpdatesPerDay,
                        _ => economyParameters.m_UnemploymentBenefit / kUpdatesPerDay,
                    };

                    if (isStudent) //Student housing stipend
                    {
                        num2 = 1000;
                    }
                }
                else
                {
                    int num3 = economyParameters.GetWage(workerData.m_Level);
                    num = TaxSystem.GetResidentialTaxRate(workerData.m_Level, m_TaxRates);
                    if (isCommuter)
                    {
                        num3 = Mathf.RoundToInt((float)num3 * economyParameters.m_CommuterWageMultiplier);
                    }
                    num2 = num3 / kUpdatesPerDay;
                    if (m_Companies.HasComponent(workplace))
                    {
                        m_PaymentQueue.Enqueue(new Payment
                        {
                            m_Target = workplace,
                            m_Amount = -num2
                        });
                    }
                }
                EconomyUtils.AddResources(Resource.Money, num2, resources);
                num2 -= economyParameters.m_ResidentialMinimumEarnings / kUpdatesPerDay;
                if (!isCommuter && num != 0 && num2 > 0)
                {
                    taxPayer.m_AverageTaxRate = Mathf.RoundToInt(math.lerp(taxPayer.m_AverageTaxRate, num, (float)num2 / (float)(num2 + taxPayer.m_UntaxedIncome)));
                    taxPayer.m_UntaxedIncome += num2;
                }
            }

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                if (m_UpdateFrameIndex != chunk.GetSharedComponent(m_UpdateFrameType).m_Index)
                {
                    return;
                }
                bool isCommuter = chunk.Has(ref m_CommuterHouseholdType);
                NativeArray<Entity> nativeArray = chunk.GetNativeArray(m_EntityType);
                NativeArray<TaxPayer> nativeArray2 = chunk.GetNativeArray(ref m_TaxPayerType);
                BufferAccessor<HouseholdCitizen> bufferAccessor = chunk.GetBufferAccessor(ref m_HouseholdCitizenType);
                BufferAccessor<Game.Economy.Resources> bufferAccessor2 = chunk.GetBufferAccessor(ref m_ResourcesType);
                for (int i = 0; i < chunk.Count; i++)
                {
                    Entity household = nativeArray[i];
                    DynamicBuffer<HouseholdCitizen> dynamicBuffer = bufferAccessor[i];
                    DynamicBuffer<Game.Economy.Resources> resources = bufferAccessor2[i];
                    TaxPayer taxPayer = nativeArray2[i];
                    for (int j = 0; j < dynamicBuffer.Length; j++)
                    {
                        Entity citizen = dynamicBuffer[j].m_Citizen;
                        Entity workplace = Entity.Null;
                        Worker workerData = default(Worker);
                        bool isStudent = false;

                        if (m_Workers.HasComponent(citizen))
                        {
                            workplace = m_Workers[citizen].m_Workplace;
                            workerData = m_Workers[citizen];
                        }

                        if(m_Students.HasComponent(citizen))
                        {
                            isStudent = true;
                        }
                        CitizenAge age = m_Citizens[citizen].GetAge();
                        PayWage(workplace, citizen, household, workerData, ref taxPayer, resources, age, isCommuter, isStudent, ref m_EconomyParameters);
                    }
                    nativeArray2[i] = taxPayer;
                }
            }

            void IJobChunk.Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                Execute(in chunk, unfilteredChunkIndex, useEnabledMask, in chunkEnabledMask);
            }
        }

        private struct TypeHandle
        {
            [ReadOnly]
            public EntityTypeHandle __Unity_Entities_Entity_TypeHandle;

            [ReadOnly]
            public BufferTypeHandle<HouseholdCitizen> __Game_Citizens_HouseholdCitizen_RO_BufferTypeHandle;

            public BufferTypeHandle<Game.Economy.Resources> __Game_Economy_Resources_RW_BufferTypeHandle;

            public ComponentTypeHandle<TaxPayer> __Game_Agents_TaxPayer_RW_ComponentTypeHandle;

            [ReadOnly]
            public ComponentTypeHandle<CommuterHousehold> __Game_Citizens_CommuterHousehold_RO_ComponentTypeHandle;

            [ReadOnly]
            public ComponentLookup<Worker> __Game_Citizens_Worker_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Citizen> __Game_Citizens_Citizen_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<CompanyData> __Game_Companies_CompanyData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Student> __Game_Citizens_Student_RO_ComponentLookup;

            public BufferLookup<Game.Economy.Resources> __Game_Economy_Resources_RW_BufferLookup;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void __AssignHandles(ref SystemState state)
            {
                __Unity_Entities_Entity_TypeHandle = state.GetEntityTypeHandle();
                __Game_Citizens_HouseholdCitizen_RO_BufferTypeHandle = state.GetBufferTypeHandle<HouseholdCitizen>(isReadOnly: true);
                __Game_Economy_Resources_RW_BufferTypeHandle = state.GetBufferTypeHandle<Game.Economy.Resources>();
                __Game_Agents_TaxPayer_RW_ComponentTypeHandle = state.GetComponentTypeHandle<TaxPayer>();
                __Game_Citizens_CommuterHousehold_RO_ComponentTypeHandle = state.GetComponentTypeHandle<CommuterHousehold>(isReadOnly: true);
                __Game_Citizens_Worker_RO_ComponentLookup = state.GetComponentLookup<Worker>(isReadOnly: true);
                __Game_Citizens_Citizen_RO_ComponentLookup = state.GetComponentLookup<Citizen>(isReadOnly: true);
                __Game_Companies_CompanyData_RO_ComponentLookup = state.GetComponentLookup<CompanyData>(isReadOnly: true);
                __Game_Economy_Resources_RW_BufferLookup = state.GetBufferLookup<Game.Economy.Resources>();
                __Game_Citizens_Student_RO_ComponentLookup = state.GetComponentLookup<Student>(isReadOnly: true);
            }
        }

        public static readonly int kUpdatesPerDay = 32;

        private SimulationSystem m_SimulationSystem;

        private TaxSystem m_TaxSystem;

        private EntityQuery m_EconomyParameterGroup;

        private EntityQuery m_HouseholdGroup;

        private NativeQueue<Payment> m_PaymentQueue;

        private TypeHandle __TypeHandle;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 262144 / (kUpdatesPerDay * 16);
        }

        [Preserve]
        protected override void OnCreate()
        {
            base.OnCreate();
            m_SimulationSystem = base.World.GetOrCreateSystemManaged<SimulationSystem>();
            m_TaxSystem = base.World.GetOrCreateSystemManaged<TaxSystem>();
            m_PaymentQueue = new NativeQueue<Payment>(Allocator.Persistent);
            m_EconomyParameterGroup = GetEntityQuery(ComponentType.ReadOnly<EconomyParameterData>());
            m_HouseholdGroup = GetEntityQuery(ComponentType.ReadOnly<Household>(), ComponentType.ReadOnly<UpdateFrame>(), ComponentType.ReadOnly<HouseholdCitizen>(), ComponentType.Exclude<TouristHousehold>(), ComponentType.Exclude<Deleted>(), ComponentType.Exclude<Temp>());
            RequireForUpdate(m_EconomyParameterGroup);
            RequireForUpdate(m_HouseholdGroup);
        }

        [Preserve]
        protected override void OnDestroy()
        {
            m_PaymentQueue.Dispose();
            base.OnDestroy();
        }

        [Preserve]
        protected override void OnUpdate()
        {
            uint updateFrame = SimulationUtils.GetUpdateFrame(m_SimulationSystem.frameIndex, kUpdatesPerDay, 16);
            __TypeHandle.__Game_Companies_CompanyData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Citizen_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Worker_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_CommuterHousehold_RO_ComponentTypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Agents_TaxPayer_RW_ComponentTypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Economy_Resources_RW_BufferTypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_HouseholdCitizen_RO_BufferTypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Unity_Entities_Entity_TypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Student_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            PayWageJob jobData = default(PayWageJob);
            jobData.m_EntityType = __TypeHandle.__Unity_Entities_Entity_TypeHandle;
            jobData.m_HouseholdCitizenType = __TypeHandle.__Game_Citizens_HouseholdCitizen_RO_BufferTypeHandle;
            jobData.m_ResourcesType = __TypeHandle.__Game_Economy_Resources_RW_BufferTypeHandle;
            jobData.m_TaxPayerType = __TypeHandle.__Game_Agents_TaxPayer_RW_ComponentTypeHandle;
            jobData.m_CommuterHouseholdType = __TypeHandle.__Game_Citizens_CommuterHousehold_RO_ComponentTypeHandle;
            jobData.m_UpdateFrameType = GetSharedComponentTypeHandle<UpdateFrame>();
            jobData.m_Workers = __TypeHandle.__Game_Citizens_Worker_RO_ComponentLookup;
            jobData.m_Citizens = __TypeHandle.__Game_Citizens_Citizen_RO_ComponentLookup;
            jobData.m_Companies = __TypeHandle.__Game_Companies_CompanyData_RO_ComponentLookup;
            jobData.m_EconomyParameters = m_EconomyParameterGroup.GetSingleton<EconomyParameterData>();
            jobData.m_UpdateFrameIndex = updateFrame;
            jobData.m_PaymentQueue = m_PaymentQueue.AsParallelWriter();
            jobData.m_TaxRates = m_TaxSystem.GetTaxRates();
            jobData.m_Students = __TypeHandle.__Game_Citizens_Student_RO_ComponentLookup;
            JobHandle jobHandle = JobChunkExtensions.ScheduleParallel(jobData, m_HouseholdGroup, base.Dependency);
            m_TaxSystem.AddReader(jobHandle);
            __TypeHandle.__Game_Economy_Resources_RW_BufferLookup.Update(ref base.CheckedStateRef);
            PayJob payJob = default(PayJob);
            payJob.m_Resources = __TypeHandle.__Game_Economy_Resources_RW_BufferLookup;
            payJob.m_PaymentQueue = m_PaymentQueue;
            PayJob jobData2 = payJob;
            base.Dependency = IJobExtensions.Schedule(jobData2, jobHandle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void __AssignQueries(ref SystemState state)
        {
        }

        protected override void OnCreateForCompiler()
        {
            base.OnCreateForCompiler();
            __AssignQueries(ref base.CheckedStateRef);
            __TypeHandle.__AssignHandles(ref base.CheckedStateRef);
        }

        [Preserve]
        public PayWageSystem()
        {
        }
    }

}
