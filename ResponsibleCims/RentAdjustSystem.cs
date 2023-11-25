// Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Game.Simulation.RentAdjustSystem
using System;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using Colossal.Entities;
using Colossal.Serialization.Entities;
using Game;
using Game.Agents;
using Game.Areas;
using Game.Buildings;
using Game.Citizens;
using Game.City;
using Game.Common;
using Game.Companies;
using Game.Economy;
using Game.Net;
using Game.Notifications;
using Game.Objects;
using Game.Prefabs;
using Game.Simulation;
using Game.Tools;
using Game.Zones;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;
using static Game.UI.MapMetadataSystem;

namespace ResponsibleCims
{

    [CompilerGenerated]
    public class RentAdjustSystem : GameSystemBase
    {
        [BurstCompile]
        private struct AdjustRentJob : IJobChunk
        {
            [ReadOnly]
            public EntityTypeHandle m_EntityType;

            public BufferTypeHandle<Renter> m_RenterType;

            [ReadOnly]
            public SharedComponentTypeHandle<UpdateFrame> m_UpdateFrameType;

            [NativeDisableParallelForRestriction]
            public ComponentLookup<PropertyRenter> m_Renters;

            [ReadOnly]
            public ComponentLookup<Household> m_Households;

            [ReadOnly]
            public ComponentLookup<Worker> m_Workers;

            [NativeDisableParallelForRestriction]
            public ComponentLookup<Building> m_Buildings;

            [ReadOnly]
            public ComponentLookup<PrefabRef> m_Prefabs;

            [ReadOnly]
            public ComponentLookup<BuildingPropertyData> m_BuildingProperties;

            [ReadOnly]
            public ComponentLookup<BuildingData> m_BuildingDatas;

            [ReadOnly]
            public ComponentLookup<WorkProvider> m_WorkProviders;

            [ReadOnly]
            public ComponentLookup<IndustrialProcessData> m_ProcessDatas;

            [ReadOnly]
            public ComponentLookup<ServiceAvailable> m_ServiceAvailables;

            [ReadOnly]
            public ComponentLookup<ServiceCompanyData> m_ServiceCompanyDatas;

            [ReadOnly]
            public ComponentLookup<Game.Companies.ProcessingCompany> m_ProcessingCompanies;

            [ReadOnly]
            public ComponentLookup<BuyingCompany> m_BuyingCompanies;

            [ReadOnly]
            public ComponentLookup<CompanyNotifications> m_CompanyNotifications;

            [ReadOnly]
            public ComponentLookup<Attached> m_Attached;

            [ReadOnly]
            public ComponentLookup<Game.Areas.Lot> m_Lots;

            [ReadOnly]
            public ComponentLookup<Geometry> m_Geometries;

            [ReadOnly]
            public ComponentLookup<Extractor> m_AreaExtractors;

            [ReadOnly]
            public ComponentLookup<LandValue> m_LandValues;

            [NativeDisableParallelForRestriction]
            public ComponentLookup<PropertyOnMarket> m_OnMarkets;

            [ReadOnly]
            public ComponentLookup<ConsumptionData> m_ConsumptionDatas;

            [ReadOnly]
            public BufferLookup<HouseholdCitizen> m_Citizens;

            [ReadOnly]
            public BufferLookup<Game.Net.ServiceCoverage> m_ServiceCoverages;

            [ReadOnly]
            public BufferLookup<ResourceAvailability> m_Availabilities;

            [ReadOnly]
            public BufferLookup<Game.Areas.SubArea> m_SubAreas;

            [ReadOnly]
            public ComponentLookup<SpawnableBuildingData> m_SpawnableBuildings;

            [ReadOnly]
            public ComponentLookup<ResourceData> m_ResourceDatas;

            [ReadOnly]
            public ComponentLookup<Game.Companies.StorageCompany> m_StorageCompanies;

            [ReadOnly]
            public ComponentLookup<WorkplaceData> m_WorkplaceDatas;

            [ReadOnly]
            public ComponentLookup<Abandoned> m_Abandoned;

            [ReadOnly]
            public ComponentLookup<Destroyed> m_Destroyed;

            [ReadOnly]
            public ComponentLookup<CrimeProducer> m_Crimes;

            [ReadOnly]
            public ComponentLookup<Game.Objects.Transform> m_Transforms;

            [ReadOnly]
            public ComponentLookup<Locked> m_Locked;

            [ReadOnly]
            public BufferLookup<CityModifier> m_CityModifiers;

            [ReadOnly]
            public ComponentLookup<CurrentDistrict> m_Districts;

            [ReadOnly]
            public BufferLookup<DistrictModifier> m_DistrictModifiers;

            [ReadOnly]
            public ComponentLookup<HealthProblem> m_HealthProblems;

            [ReadOnly]
            public BufferLookup<Employee> m_Employees;

            [ReadOnly]
            public BufferLookup<Efficiency> m_BuildingEfficiencies;

            [ReadOnly]
            public ComponentLookup<ExtractorAreaData> m_ExtractorDatas;

            [ReadOnly]
            public ComponentLookup<Citizen> m_CitizenDatas;

            [ReadOnly]
            public ComponentLookup<Game.Citizens.Student> m_Students;

            [ReadOnly]
            public ComponentLookup<SpawnableBuildingData> m_SpawnableBuildingData;

            [ReadOnly]
            public ComponentLookup<ZoneData> m_ZoneData;

            [ReadOnly]
            public BufferLookup<TradeCost> m_TradeCosts;

            [ReadOnly]
            public ComponentLookup<ElectricityConsumer> m_ElectricityConsumers;

            [ReadOnly]
            public ComponentLookup<AdjustHappinessData> m_AdjustHappinessDatas;

            [ReadOnly]
            public ComponentLookup<WaterConsumer> m_WaterConsumers;

            [ReadOnly]
            public ComponentLookup<GarbageProducer> m_GarbageProducers;

            [ReadOnly]
            public ComponentLookup<MailProducer> m_MailProducers;

            [ReadOnly]
            public ComponentLookup<Abandoned> m_Abandoneds;

            [ReadOnly]
            public ComponentLookup<ExtractorProperty> m_ExtractorProperties;

            [NativeDisableParallelForRestriction]
            public ComponentLookup<BuildingNotifications> m_BuildingNotifications;

            [ReadOnly]
            public NativeArray<AirPollution> m_AirPollutionMap;

            [ReadOnly]
            public NativeArray<GroundPollution> m_PollutionMap;

            [ReadOnly]
            public NativeArray<NoisePollution> m_NoiseMap;

            [ReadOnly]
            public CellMapData<TelecomCoverage> m_TelecomCoverages;

            [ReadOnly]
            public NativeArray<int> m_Unemployment;

            [ReadOnly]
            public BufferLookup<Game.Economy.Resources> m_Resources;

            [ReadOnly]
            public ComponentLookup<HouseholdMember> m_HouseholdMembers;

            public ExtractorParameterData m_ExtractorParameters;

            public HealthcareParameterData m_HealthcareParameters;

            public ParkParameterData m_ParkParameters;

            public EducationParameterData m_EducationParameters;

            public TelecomParameterData m_TelecomParameters;

            public GarbageParameterData m_GarbageParameters;

            public PoliceConfigurationData m_PoliceParameters;

            public CitizenHappinessParameterData m_CitizenHappinessParameterData;

            public BuildingConfigurationData m_BuildingConfigurationData;

            public PollutionParameterData m_PollutionParameters;

            public IconCommandBuffer m_IconCommandBuffer;

            [ReadOnly]
            public NativeArray<int> m_TaxRates;

            [ReadOnly]
            public ResourcePrefabs m_ResourcePrefabs;

            public uint m_UpdateFrameIndex;

            public float m_BaseConsumptionSum;

            [ReadOnly]
            public Entity m_City;

            public EconomyParameterData m_EconomyParameters;

            public DemandParameterData m_DemandParameters;

            public EntityCommandBuffer.ParallelWriter m_CommandBuffer;

            private bool CanDisplayHighRentWarnIcon(DynamicBuffer<Renter> renters)
            {
                bool result = true;
                for (int i = 0; i < renters.Length; i++)
                {
                    Entity renter = renters[i].m_Renter;
                    if (m_CompanyNotifications.HasComponent(renter))
                    {
                        CompanyNotifications companyNotifications = m_CompanyNotifications[renter];
                        if (companyNotifications.m_NoCustomersEntity != Entity.Null || companyNotifications.m_NoInputEntity != Entity.Null)
                        {
                            result = false;
                            break;
                        }
                    }
                    if (m_WorkProviders.HasComponent(renter))
                    {
                        WorkProvider workProvider = m_WorkProviders[renter];
                        if (workProvider.m_EducatedNotificationEntity != Entity.Null || workProvider.m_UneducatedNotificationEntity != Entity.Null)
                        {
                            result = false;
                            break;
                        }
                    }
                    if (!m_Citizens.HasBuffer(renter))
                    {
                        continue;
                    }
                    DynamicBuffer<HouseholdCitizen> dynamicBuffer = m_Citizens[renter];
                    result = false;
                    for (int j = 0; j < dynamicBuffer.Length; j++)
                    {
                        Entity entity = dynamicBuffer[j].m_Citizen;
                        Household household = m_Households[entity];
                        Entity householdEntity = m_HouseholdMembers[entity].m_Household;
                        int cash = 0;

                        
                        if (!householdEntity.Equals(Entity.Null))
                        {
                            if (m_Resources.HasBuffer(householdEntity))
                            {
                                DynamicBuffer<Game.Economy.Resources> resources = m_Resources[householdEntity];
                                cash = EconomyUtils.GetResources(Resource.Money, resources);
                            }
                        }

                        if (cash >= 1000) //Suppress the high rent warning if household cash reserves are higher than $1,000
                        {
                            result = false;
                            break;
                        }

                        if (!CitizenUtils.IsDead(dynamicBuffer[j].m_Citizen, ref m_HealthProblems))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }

            public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
            {
                if (chunk.GetSharedComponent(m_UpdateFrameType).m_Index != m_UpdateFrameIndex)
                {
                    return;
                }
                NativeArray<Entity> nativeArray = chunk.GetNativeArray(m_EntityType);
                BufferAccessor<Renter> bufferAccessor = chunk.GetBufferAccessor(ref m_RenterType);
                DynamicBuffer<CityModifier> cityModifiers = m_CityModifiers[m_City];
                for (int i = 0; i < nativeArray.Length; i++)
                {
                    Entity entity = nativeArray[i];
                    Entity prefab = m_Prefabs[entity].m_Prefab;
                    if (!m_BuildingProperties.HasComponent(prefab))
                    {
                        continue;
                    }
                    BuildingPropertyData buildingProperties = m_BuildingProperties[prefab];
                    bool flag = buildingProperties.m_ResidentialProperties > 0 && (buildingProperties.m_AllowedSold != Resource.NoResource || buildingProperties.m_AllowedManufactured != Resource.NoResource);
                    Building value = m_Buildings[entity];
                    DynamicBuffer<Renter> renters = bufferAccessor[i];
                    BuildingData buildingData = m_BuildingDatas[prefab];
                    int num = buildingData.m_LotSize.x * buildingData.m_LotSize.y;
                    if (m_Attached.HasComponent(entity))
                    {
                        Entity parent = m_Attached[entity].m_Parent;
                        float area = ExtractorAISystem.GetArea(m_SubAreas[parent], m_Lots, m_Geometries);
                        num += Mathf.CeilToInt(area);
                    }
                    float landValue = 0f;
                    if (m_LandValues.HasComponent(value.m_RoadEdge))
                    {
                        landValue = m_LandValues[value.m_RoadEdge].m_LandValue;
                        landValue *= (float)num;
                    }
                    Game.Zones.AreaType areaType = Game.Zones.AreaType.None;
                    if (m_SpawnableBuildingData.HasComponent(prefab))
                    {
                        SpawnableBuildingData spawnableBuildingData = m_SpawnableBuildingData[prefab];
                        areaType = m_ZoneData[spawnableBuildingData.m_ZonePrefab].m_AreaType;
                    }
                    if (areaType == Game.Zones.AreaType.Residential)
                    {
                        int2 groundPollutionBonuses = CitizenHappinessSystem.GetGroundPollutionBonuses(entity, ref m_Transforms, m_PollutionMap, cityModifiers, in m_CitizenHappinessParameterData);
                        int2 noiseBonuses = CitizenHappinessSystem.GetNoiseBonuses(entity, ref m_Transforms, m_NoiseMap, in m_CitizenHappinessParameterData);
                        int2 airPollutionBonuses = CitizenHappinessSystem.GetAirPollutionBonuses(entity, ref m_Transforms, m_AirPollutionMap, cityModifiers, in m_CitizenHappinessParameterData);
                        bool flag2 = groundPollutionBonuses.x + groundPollutionBonuses.y < 2 * m_PollutionParameters.m_GroundPollutionNotificationLimit;
                        bool flag3 = airPollutionBonuses.x + airPollutionBonuses.y < 2 * m_PollutionParameters.m_AirPollutionNotificationLimit;
                        bool flag4 = noiseBonuses.x + noiseBonuses.y < 2 * m_PollutionParameters.m_NoisePollutionNotificationLimit;
                        BuildingNotifications value2 = m_BuildingNotifications[entity];
                        if (flag2 && !value2.HasNotification(BuildingNotification.GroundPollution))
                        {
                            m_IconCommandBuffer.Add(entity, m_PollutionParameters.m_GroundPollutionNotification, IconPriority.Problem);
                            value2.m_Notifications |= BuildingNotification.GroundPollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                        else if (!flag2 && value2.HasNotification(BuildingNotification.GroundPollution))
                        {
                            m_IconCommandBuffer.Remove(entity, m_PollutionParameters.m_GroundPollutionNotification);
                            value2.m_Notifications &= ~BuildingNotification.GroundPollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                        if (flag3 && !value2.HasNotification(BuildingNotification.AirPollution))
                        {
                            m_IconCommandBuffer.Add(entity, m_PollutionParameters.m_AirPollutionNotification, IconPriority.Problem);
                            value2.m_Notifications |= BuildingNotification.AirPollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                        else if (!flag3 && value2.HasNotification(BuildingNotification.AirPollution))
                        {
                            m_IconCommandBuffer.Remove(entity, m_PollutionParameters.m_AirPollutionNotification);
                            value2.m_Notifications &= ~BuildingNotification.AirPollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                        if (flag4 && !value2.HasNotification(BuildingNotification.NoisePollution))
                        {
                            m_IconCommandBuffer.Add(entity, m_PollutionParameters.m_NoisePollutionNotification, IconPriority.Problem);
                            value2.m_Notifications |= BuildingNotification.NoisePollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                        else if (!flag4 && value2.HasNotification(BuildingNotification.NoisePollution))
                        {
                            m_IconCommandBuffer.Remove(entity, m_PollutionParameters.m_NoisePollutionNotification);
                            value2.m_Notifications &= ~BuildingNotification.NoisePollution;
                            m_BuildingNotifications[entity] = value2;
                        }
                    }
                    int2 rent = GetRent(m_ConsumptionDatas[prefab], buildingProperties, landValue, areaType);
                    if (m_OnMarkets.HasComponent(entity))
                    {
                        PropertyOnMarket value3 = m_OnMarkets[entity];
                        value3.m_AskingRent = rent.x;
                        m_OnMarkets[entity] = value3;
                    }
                    int num2 = buildingProperties.CountProperties();
                    bool flag5 = false;
                    Entity healthcareServicePrefab = m_HealthcareParameters.m_HealthcareServicePrefab;
                    Entity parkServicePrefab = m_ParkParameters.m_ParkServicePrefab;
                    Entity educationServicePrefab = m_EducationParameters.m_EducationServicePrefab;
                    Entity telecomServicePrefab = m_TelecomParameters.m_TelecomServicePrefab;
                    Entity garbageServicePrefab = m_GarbageParameters.m_GarbageServicePrefab;
                    Entity policeServicePrefab = m_PoliceParameters.m_PoliceServicePrefab;
                    int2 @int = default(int2);
                    bool flag6 = false;
                    bool flag7 = m_ExtractorProperties.HasComponent(entity);
                    float num3 = ((num2 > 1) ? math.saturate((float)renters.Length / (float)num2) : 0f);
                    for (int num4 = renters.Length - 1; num4 >= 0; num4--)
                    {
                        Entity renter = renters[num4].m_Renter;
                        if (m_Renters.HasComponent(renter))
                        {
                            int2 int2 = rent;
                            PropertyRenter propertyRenter = m_Renters[renter];
                            int2 int3 = CalculateMaximumRent(renter, ref m_EconomyParameters, ref m_DemandParameters, m_BaseConsumptionSum, cityModifiers, propertyRenter, healthcareServicePrefab, parkServicePrefab, educationServicePrefab, telecomServicePrefab, garbageServicePrefab, policeServicePrefab, ref m_Households, ref m_Workers, ref m_Buildings, ref m_Citizens, ref m_Prefabs, ref m_Availabilities, ref m_BuildingProperties, ref m_BuildingDatas, ref m_SpawnableBuildings, ref m_Crimes, ref m_ServiceCoverages, ref m_Locked, ref m_ElectricityConsumers, ref m_AdjustHappinessDatas, ref m_WaterConsumers, ref m_GarbageProducers, ref m_MailProducers, ref m_Transforms, m_PollutionMap, m_AirPollutionMap, m_NoiseMap, m_TelecomCoverages, m_ResourcePrefabs, ref m_ResourceDatas, ref m_ProcessDatas, ref m_StorageCompanies, ref m_ServiceAvailables, ref m_WorkProviders, ref m_ServiceCompanyDatas, ref m_WorkplaceDatas, ref m_ProcessingCompanies, ref m_BuyingCompanies, ref m_SubAreas, ref m_Attached, ref m_Lots, ref m_Geometries, ref m_AreaExtractors, ref m_HealthProblems, m_CitizenHappinessParameterData, m_GarbageParameters, m_TaxRates, ref m_Districts, ref m_DistrictModifiers, ref m_Employees, ref m_BuildingEfficiencies, ref m_ExtractorDatas, m_ExtractorParameters, ref m_CitizenDatas, ref m_Students, m_Unemployment, ref m_TradeCosts, ref m_Abandoneds);
                            if (flag && !m_Households.HasComponent(renter))
                            {
                                int2.x = Mathf.RoundToInt(kMixedCompanyRent * ((float)(int2.x * buildingProperties.m_ResidentialProperties) / (1f - kMixedCompanyRent)));
                                int2.y = Mathf.RoundToInt(kMixedCompanyRent * ((float)(int2.y * buildingProperties.m_ResidentialProperties) / (1f - kMixedCompanyRent)));
                            }
                            if (int2.x > int3.x)
                            {
                                float s = 0.2f + 0.3f * num3 * num3;
                                int2.x = Mathf.RoundToInt(math.max(math.lerp(int2.y, int2.x, s), int3.x));
                            }
                            propertyRenter.m_Rent = int2.x;
                            propertyRenter.m_MaxRent = int3.x;
                            m_Renters[renter] = propertyRenter;
                            flag6 |= m_StorageCompanies.HasComponent(renter);
                            if (int2.x > int3.y && !m_StorageCompanies.HasComponent(renter))
                            {
                                m_CommandBuffer.AddComponent(unfilteredChunkIndex, renter, default(PropertySeeker));
                            }
                            @int.y++;
                            if (propertyRenter.m_Rent > int3.y)
                            {
                                @int.x++;
                            }
                        }
                        else
                        {
                            renters.RemoveAt(num4);
                            flag5 = true;
                        }
                    }
                    if (!((float)@int.x / math.max(1f, @int.y) > 0.7f) || !CanDisplayHighRentWarnIcon(renters))
                    {
                        m_IconCommandBuffer.Remove(entity, m_BuildingConfigurationData.m_HighRentNotification);
                        value.m_Flags &= ~Game.Buildings.BuildingFlags.HighRentWarning;
                        m_Buildings[entity] = value;
                    }
                    else if (renters.Length > 0 && !flag7 && (!flag6 || num2 > renters.Length) && (value.m_Flags & Game.Buildings.BuildingFlags.HighRentWarning) == 0)
                    {
                        m_IconCommandBuffer.Add(entity, m_BuildingConfigurationData.m_HighRentNotification, IconPriority.Problem);
                        value.m_Flags |= Game.Buildings.BuildingFlags.HighRentWarning;
                        m_Buildings[entity] = value;
                    }
                    if (renters.Length > num2 && m_Renters.HasComponent(renters[renters.Length - 1].m_Renter))
                    {
                        m_CommandBuffer.RemoveComponent<PropertyRenter>(unfilteredChunkIndex, renters[renters.Length - 1].m_Renter);
                        renters.RemoveAt(renters.Length - 1);
                    }
                    if (renters.Length == 0 && (value.m_Flags & Game.Buildings.BuildingFlags.HighRentWarning) != 0)
                    {
                        m_IconCommandBuffer.Remove(entity, m_BuildingConfigurationData.m_HighRentNotification);
                        value.m_Flags &= ~Game.Buildings.BuildingFlags.HighRentWarning;
                        m_Buildings[entity] = value;
                    }
                    if (m_Prefabs.HasComponent(entity) && !m_Abandoned.HasComponent(entity) && !m_Destroyed.HasComponent(entity) && flag5 && num2 > renters.Length)
                    {
                        m_CommandBuffer.AddComponent(unfilteredChunkIndex, entity, new PropertyOnMarket
                        {
                            m_AskingRent = rent.x
                        });
                    }
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

            public BufferTypeHandle<Renter> __Game_Buildings_Renter_RW_BufferTypeHandle;

            public ComponentLookup<PropertyRenter> __Game_Buildings_PropertyRenter_RW_ComponentLookup;

            public ComponentLookup<PropertyOnMarket> __Game_Buildings_PropertyOnMarket_RW_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Household> __Game_Citizens_Household_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Worker> __Game_Citizens_Worker_RO_ComponentLookup;

            public ComponentLookup<Building> __Game_Buildings_Building_RW_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<PrefabRef> __Game_Prefabs_PrefabRef_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<BuildingPropertyData> __Game_Prefabs_BuildingPropertyData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<BuildingData> __Game_Prefabs_BuildingData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<WorkProvider> __Game_Companies_WorkProvider_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<IndustrialProcessData> __Game_Prefabs_IndustrialProcessData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ServiceAvailable> __Game_Companies_ServiceAvailable_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ServiceCompanyData> __Game_Companies_ServiceCompanyData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Game.Companies.ProcessingCompany> __Game_Companies_ProcessingCompany_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<BuyingCompany> __Game_Companies_BuyingCompany_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<CompanyNotifications> __Game_Companies_CompanyNotifications_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Attached> __Game_Objects_Attached_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Game.Areas.Lot> __Game_Areas_Lot_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Geometry> __Game_Areas_Geometry_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Extractor> __Game_Areas_Extractor_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<LandValue> __Game_Net_LandValue_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ConsumptionData> __Game_Prefabs_ConsumptionData_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<HouseholdCitizen> __Game_Citizens_HouseholdCitizen_RO_BufferLookup;

            [ReadOnly]
            public BufferLookup<Game.Net.ServiceCoverage> __Game_Net_ServiceCoverage_RO_BufferLookup;

            [ReadOnly]
            public BufferLookup<ResourceAvailability> __Game_Net_ResourceAvailability_RO_BufferLookup;

            [ReadOnly]
            public BufferLookup<Game.Areas.SubArea> __Game_Areas_SubArea_RO_BufferLookup;

            [ReadOnly]
            public ComponentLookup<SpawnableBuildingData> __Game_Prefabs_SpawnableBuildingData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ResourceData> __Game_Prefabs_ResourceData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Game.Companies.StorageCompany> __Game_Companies_StorageCompany_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<WorkplaceData> __Game_Prefabs_WorkplaceData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Abandoned> __Game_Buildings_Abandoned_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Destroyed> __Game_Common_Destroyed_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<CrimeProducer> __Game_Buildings_CrimeProducer_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Locked> __Game_Prefabs_Locked_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Game.Objects.Transform> __Game_Objects_Transform_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<CityModifier> __Game_City_CityModifier_RO_BufferLookup;

            [ReadOnly]
            public ComponentLookup<CurrentDistrict> __Game_Areas_CurrentDistrict_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<DistrictModifier> __Game_Areas_DistrictModifier_RO_BufferLookup;

            [ReadOnly]
            public ComponentLookup<HealthProblem> __Game_Citizens_HealthProblem_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<Employee> __Game_Companies_Employee_RO_BufferLookup;

            [ReadOnly]
            public BufferLookup<Efficiency> __Game_Buildings_Efficiency_RO_BufferLookup;

            [ReadOnly]
            public ComponentLookup<ExtractorAreaData> __Game_Prefabs_ExtractorAreaData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Citizen> __Game_Citizens_Citizen_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<Game.Citizens.Student> __Game_Citizens_Student_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ZoneData> __Game_Prefabs_ZoneData_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<TradeCost> __Game_Companies_TradeCost_RO_BufferLookup;

            public ComponentLookup<BuildingNotifications> __Game_Buildings_BuildingNotifications_RW_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ElectricityConsumer> __Game_Buildings_ElectricityConsumer_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<AdjustHappinessData> __Game_Prefabs_AdjustHappinessData_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<WaterConsumer> __Game_Buildings_WaterConsumer_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<GarbageProducer> __Game_Buildings_GarbageProducer_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<MailProducer> __Game_Buildings_MailProducer_RO_ComponentLookup;

            [ReadOnly]
            public ComponentLookup<ExtractorProperty> __Game_Buildings_ExtractorProperty_RO_ComponentLookup;

            [ReadOnly]
            public BufferLookup<Game.Economy.Resources> __Game_Economy_Resources_RO_BufferLookup;

            [ReadOnly]
            public ComponentLookup<HouseholdMember> __Game_Citizens_HouseholdMember_RO_ComponentLookup;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void __AssignHandles(ref SystemState state)
            {
                __Unity_Entities_Entity_TypeHandle = state.GetEntityTypeHandle();
                __Game_Buildings_Renter_RW_BufferTypeHandle = state.GetBufferTypeHandle<Renter>();
                __Game_Buildings_PropertyRenter_RW_ComponentLookup = state.GetComponentLookup<PropertyRenter>();
                __Game_Buildings_PropertyOnMarket_RW_ComponentLookup = state.GetComponentLookup<PropertyOnMarket>();
                __Game_Citizens_Household_RO_ComponentLookup = state.GetComponentLookup<Household>(isReadOnly: true);
                __Game_Citizens_Worker_RO_ComponentLookup = state.GetComponentLookup<Worker>(isReadOnly: true);
                __Game_Buildings_Building_RW_ComponentLookup = state.GetComponentLookup<Building>();
                __Game_Prefabs_PrefabRef_RO_ComponentLookup = state.GetComponentLookup<PrefabRef>(isReadOnly: true);
                __Game_Prefabs_BuildingPropertyData_RO_ComponentLookup = state.GetComponentLookup<BuildingPropertyData>(isReadOnly: true);
                __Game_Prefabs_BuildingData_RO_ComponentLookup = state.GetComponentLookup<BuildingData>(isReadOnly: true);
                __Game_Companies_WorkProvider_RO_ComponentLookup = state.GetComponentLookup<WorkProvider>(isReadOnly: true);
                __Game_Prefabs_IndustrialProcessData_RO_ComponentLookup = state.GetComponentLookup<IndustrialProcessData>(isReadOnly: true);
                __Game_Companies_ServiceAvailable_RO_ComponentLookup = state.GetComponentLookup<ServiceAvailable>(isReadOnly: true);
                __Game_Companies_ServiceCompanyData_RO_ComponentLookup = state.GetComponentLookup<ServiceCompanyData>(isReadOnly: true);
                __Game_Companies_ProcessingCompany_RO_ComponentLookup = state.GetComponentLookup<Game.Companies.ProcessingCompany>(isReadOnly: true);
                __Game_Companies_BuyingCompany_RO_ComponentLookup = state.GetComponentLookup<BuyingCompany>(isReadOnly: true);
                __Game_Companies_CompanyNotifications_RO_ComponentLookup = state.GetComponentLookup<CompanyNotifications>(isReadOnly: true);
                __Game_Objects_Attached_RO_ComponentLookup = state.GetComponentLookup<Attached>(isReadOnly: true);
                __Game_Areas_Lot_RO_ComponentLookup = state.GetComponentLookup<Game.Areas.Lot>(isReadOnly: true);
                __Game_Areas_Geometry_RO_ComponentLookup = state.GetComponentLookup<Geometry>(isReadOnly: true);
                __Game_Areas_Extractor_RO_ComponentLookup = state.GetComponentLookup<Extractor>(isReadOnly: true);
                __Game_Net_LandValue_RO_ComponentLookup = state.GetComponentLookup<LandValue>(isReadOnly: true);
                __Game_Prefabs_ConsumptionData_RO_ComponentLookup = state.GetComponentLookup<ConsumptionData>(isReadOnly: true);
                __Game_Citizens_HouseholdCitizen_RO_BufferLookup = state.GetBufferLookup<HouseholdCitizen>(isReadOnly: true);
                __Game_Net_ServiceCoverage_RO_BufferLookup = state.GetBufferLookup<Game.Net.ServiceCoverage>(isReadOnly: true);
                __Game_Net_ResourceAvailability_RO_BufferLookup = state.GetBufferLookup<ResourceAvailability>(isReadOnly: true);
                __Game_Areas_SubArea_RO_BufferLookup = state.GetBufferLookup<Game.Areas.SubArea>(isReadOnly: true);
                __Game_Prefabs_SpawnableBuildingData_RO_ComponentLookup = state.GetComponentLookup<SpawnableBuildingData>(isReadOnly: true);
                __Game_Prefabs_ResourceData_RO_ComponentLookup = state.GetComponentLookup<ResourceData>(isReadOnly: true);
                __Game_Companies_StorageCompany_RO_ComponentLookup = state.GetComponentLookup<Game.Companies.StorageCompany>(isReadOnly: true);
                __Game_Prefabs_WorkplaceData_RO_ComponentLookup = state.GetComponentLookup<WorkplaceData>(isReadOnly: true);
                __Game_Buildings_Abandoned_RO_ComponentLookup = state.GetComponentLookup<Abandoned>(isReadOnly: true);
                __Game_Common_Destroyed_RO_ComponentLookup = state.GetComponentLookup<Destroyed>(isReadOnly: true);
                __Game_Buildings_CrimeProducer_RO_ComponentLookup = state.GetComponentLookup<CrimeProducer>(isReadOnly: true);
                __Game_Prefabs_Locked_RO_ComponentLookup = state.GetComponentLookup<Locked>(isReadOnly: true);
                __Game_Objects_Transform_RO_ComponentLookup = state.GetComponentLookup<Game.Objects.Transform>(isReadOnly: true);
                __Game_City_CityModifier_RO_BufferLookup = state.GetBufferLookup<CityModifier>(isReadOnly: true);
                __Game_Areas_CurrentDistrict_RO_ComponentLookup = state.GetComponentLookup<CurrentDistrict>(isReadOnly: true);
                __Game_Areas_DistrictModifier_RO_BufferLookup = state.GetBufferLookup<DistrictModifier>(isReadOnly: true);
                __Game_Citizens_HealthProblem_RO_ComponentLookup = state.GetComponentLookup<HealthProblem>(isReadOnly: true);
                __Game_Companies_Employee_RO_BufferLookup = state.GetBufferLookup<Employee>(isReadOnly: true);
                __Game_Buildings_Efficiency_RO_BufferLookup = state.GetBufferLookup<Efficiency>(isReadOnly: true);
                __Game_Prefabs_ExtractorAreaData_RO_ComponentLookup = state.GetComponentLookup<ExtractorAreaData>(isReadOnly: true);
                __Game_Citizens_Citizen_RO_ComponentLookup = state.GetComponentLookup<Citizen>(isReadOnly: true);
                __Game_Citizens_Student_RO_ComponentLookup = state.GetComponentLookup<Game.Citizens.Student>(isReadOnly: true);
                __Game_Prefabs_ZoneData_RO_ComponentLookup = state.GetComponentLookup<ZoneData>(isReadOnly: true);
                __Game_Companies_TradeCost_RO_BufferLookup = state.GetBufferLookup<TradeCost>(isReadOnly: true);
                __Game_Buildings_BuildingNotifications_RW_ComponentLookup = state.GetComponentLookup<BuildingNotifications>();
                __Game_Buildings_ElectricityConsumer_RO_ComponentLookup = state.GetComponentLookup<ElectricityConsumer>(isReadOnly: true);
                __Game_Prefabs_AdjustHappinessData_RO_ComponentLookup = state.GetComponentLookup<AdjustHappinessData>(isReadOnly: true);
                __Game_Buildings_WaterConsumer_RO_ComponentLookup = state.GetComponentLookup<WaterConsumer>(isReadOnly: true);
                __Game_Buildings_GarbageProducer_RO_ComponentLookup = state.GetComponentLookup<GarbageProducer>(isReadOnly: true);
                __Game_Buildings_MailProducer_RO_ComponentLookup = state.GetComponentLookup<MailProducer>(isReadOnly: true);
                __Game_Buildings_ExtractorProperty_RO_ComponentLookup = state.GetComponentLookup<ExtractorProperty>(isReadOnly: true);
                __Game_Economy_Resources_RO_BufferLookup = state.GetBufferLookup<Game.Economy.Resources>(isReadOnly: true);
                __Game_Citizens_HouseholdMember_RO_ComponentLookup = state.GetComponentLookup<HouseholdMember>(isReadOnly: true);
            }
        }

        public static readonly int kUpdatesPerDay = 16;

        public static readonly float kMixedCompanyRent = 0.4f;

        private EntityQuery m_EconomyParameterQuery;

        private EntityQuery m_DemandParameterQuery;

        private SimulationSystem m_SimulationSystem;

        private EndFrameBarrier m_EndFrameBarrier;

        private ResourceSystem m_ResourceSystem;

        private GroundPollutionSystem m_GroundPollutionSystem;

        private AirPollutionSystem m_AirPollutionSystem;

        private NoisePollutionSystem m_NoisePollutionSystem;

        private TelecomCoverageSystem m_TelecomCoverageSystem;

        private CitySystem m_CitySystem;

        private TaxSystem m_TaxSystem;

        private CountEmploymentSystem m_CountEmploymentSystem;

        private IconCommandSystem m_IconCommandSystem;

        private EntityQuery m_HealthcareParameterQuery;

        private EntityQuery m_ExtractorParameterQuery;

        private EntityQuery m_ParkParameterQuery;

        private EntityQuery m_EducationParameterQuery;

        private EntityQuery m_TelecomParameterQuery;

        private EntityQuery m_GarbageParameterQuery;

        private EntityQuery m_PoliceParameterQuery;

        private EntityQuery m_CitizenHappinessParameterQuery;

        private EntityQuery m_BuildingParameterQuery;

        private EntityQuery m_PollutionParameterQuery;

        private EntityQuery m_BuildingQuery;

        protected int cycles;

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
            m_EndFrameBarrier = base.World.GetOrCreateSystemManaged<EndFrameBarrier>();
            m_ResourceSystem = base.World.GetOrCreateSystemManaged<ResourceSystem>();
            m_GroundPollutionSystem = base.World.GetOrCreateSystemManaged<GroundPollutionSystem>();
            m_AirPollutionSystem = base.World.GetOrCreateSystemManaged<AirPollutionSystem>();
            m_NoisePollutionSystem = base.World.GetOrCreateSystemManaged<NoisePollutionSystem>();
            m_TelecomCoverageSystem = base.World.GetOrCreateSystemManaged<TelecomCoverageSystem>();
            m_CitySystem = base.World.GetOrCreateSystemManaged<CitySystem>();
            m_TaxSystem = base.World.GetOrCreateSystemManaged<TaxSystem>();
            m_CountEmploymentSystem = base.World.GetOrCreateSystemManaged<CountEmploymentSystem>();
            m_IconCommandSystem = base.World.GetOrCreateSystemManaged<IconCommandSystem>();
            m_EconomyParameterQuery = GetEntityQuery(ComponentType.ReadOnly<EconomyParameterData>());
            m_DemandParameterQuery = GetEntityQuery(ComponentType.ReadOnly<DemandParameterData>());
            m_BuildingParameterQuery = GetEntityQuery(ComponentType.ReadOnly<BuildingConfigurationData>());
            m_BuildingQuery = GetEntityQuery(ComponentType.ReadOnly<Building>(), ComponentType.ReadOnly<UpdateFrame>(), ComponentType.ReadWrite<Renter>(), ComponentType.Exclude<Temp>(), ComponentType.Exclude<Deleted>());
            m_ExtractorParameterQuery = GetEntityQuery(ComponentType.ReadOnly<ExtractorParameterData>());
            m_HealthcareParameterQuery = GetEntityQuery(ComponentType.ReadOnly<HealthcareParameterData>());
            m_ParkParameterQuery = GetEntityQuery(ComponentType.ReadOnly<ParkParameterData>());
            m_EducationParameterQuery = GetEntityQuery(ComponentType.ReadOnly<EducationParameterData>());
            m_TelecomParameterQuery = GetEntityQuery(ComponentType.ReadOnly<TelecomParameterData>());
            m_GarbageParameterQuery = GetEntityQuery(ComponentType.ReadOnly<GarbageParameterData>());
            m_PoliceParameterQuery = GetEntityQuery(ComponentType.ReadOnly<PoliceConfigurationData>());
            m_CitizenHappinessParameterQuery = GetEntityQuery(ComponentType.ReadOnly<CitizenHappinessParameterData>());
            m_PollutionParameterQuery = GetEntityQuery(ComponentType.ReadOnly<PollutionParameterData>());
            RequireForUpdate(m_EconomyParameterQuery);
            RequireForUpdate(m_DemandParameterQuery);
            RequireForUpdate(m_HealthcareParameterQuery);
            RequireForUpdate(m_ParkParameterQuery);
            RequireForUpdate(m_EducationParameterQuery);
            RequireForUpdate(m_TelecomParameterQuery);
            RequireForUpdate(m_GarbageParameterQuery);
            RequireForUpdate(m_PoliceParameterQuery);
            RequireForUpdate(m_BuildingQuery);
        }

        [Preserve]
        protected override void OnUpdate()
        {
            uint updateFrame = SimulationUtils.GetUpdateFrame(m_SimulationSystem.frameIndex, kUpdatesPerDay, 16);
            __TypeHandle.__Game_Buildings_ExtractorProperty_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_MailProducer_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_GarbageProducer_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_WaterConsumer_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_AdjustHappinessData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_ElectricityConsumer_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_BuildingNotifications_RW_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_TradeCost_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_ZoneData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Student_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Citizen_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_ExtractorAreaData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_Efficiency_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_Employee_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_HealthProblem_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_DistrictModifier_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_CurrentDistrict_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_City_CityModifier_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Objects_Transform_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_Locked_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_CrimeProducer_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Common_Destroyed_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_Abandoned_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_WorkplaceData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_StorageCompany_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_ResourceData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_SpawnableBuildingData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_SubArea_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Net_ResourceAvailability_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Net_ServiceCoverage_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_HouseholdCitizen_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_ConsumptionData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Net_LandValue_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_Extractor_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_Geometry_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Areas_Lot_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Objects_Attached_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_CompanyNotifications_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_BuyingCompany_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_ProcessingCompany_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_ServiceCompanyData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_ServiceAvailable_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_IndustrialProcessData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Companies_WorkProvider_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_BuildingData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_BuildingPropertyData_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Prefabs_PrefabRef_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_Building_RW_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Worker_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_Household_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_PropertyOnMarket_RW_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_PropertyRenter_RW_ComponentLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Buildings_Renter_RW_BufferTypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Unity_Entities_Entity_TypeHandle.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Economy_Resources_RO_BufferLookup.Update(ref base.CheckedStateRef);
            __TypeHandle.__Game_Citizens_HouseholdMember_RO_ComponentLookup.Update(ref base.CheckedStateRef);
            AdjustRentJob jobData = default(AdjustRentJob);
            jobData.m_EntityType = __TypeHandle.__Unity_Entities_Entity_TypeHandle;
            jobData.m_RenterType = __TypeHandle.__Game_Buildings_Renter_RW_BufferTypeHandle;
            jobData.m_UpdateFrameType = GetSharedComponentTypeHandle<UpdateFrame>();
            jobData.m_Renters = __TypeHandle.__Game_Buildings_PropertyRenter_RW_ComponentLookup;
            jobData.m_OnMarkets = __TypeHandle.__Game_Buildings_PropertyOnMarket_RW_ComponentLookup;
            jobData.m_Households = __TypeHandle.__Game_Citizens_Household_RO_ComponentLookup;
            jobData.m_Workers = __TypeHandle.__Game_Citizens_Worker_RO_ComponentLookup;
            jobData.m_Buildings = __TypeHandle.__Game_Buildings_Building_RW_ComponentLookup;
            jobData.m_Prefabs = __TypeHandle.__Game_Prefabs_PrefabRef_RO_ComponentLookup;
            jobData.m_BuildingProperties = __TypeHandle.__Game_Prefabs_BuildingPropertyData_RO_ComponentLookup;
            jobData.m_BuildingDatas = __TypeHandle.__Game_Prefabs_BuildingData_RO_ComponentLookup;
            jobData.m_WorkProviders = __TypeHandle.__Game_Companies_WorkProvider_RO_ComponentLookup;
            jobData.m_ProcessDatas = __TypeHandle.__Game_Prefabs_IndustrialProcessData_RO_ComponentLookup;
            jobData.m_ServiceAvailables = __TypeHandle.__Game_Companies_ServiceAvailable_RO_ComponentLookup;
            jobData.m_ServiceCompanyDatas = __TypeHandle.__Game_Companies_ServiceCompanyData_RO_ComponentLookup;
            jobData.m_ProcessingCompanies = __TypeHandle.__Game_Companies_ProcessingCompany_RO_ComponentLookup;
            jobData.m_BuyingCompanies = __TypeHandle.__Game_Companies_BuyingCompany_RO_ComponentLookup;
            jobData.m_CompanyNotifications = __TypeHandle.__Game_Companies_CompanyNotifications_RO_ComponentLookup;
            jobData.m_Attached = __TypeHandle.__Game_Objects_Attached_RO_ComponentLookup;
            jobData.m_Lots = __TypeHandle.__Game_Areas_Lot_RO_ComponentLookup;
            jobData.m_Geometries = __TypeHandle.__Game_Areas_Geometry_RO_ComponentLookup;
            jobData.m_AreaExtractors = __TypeHandle.__Game_Areas_Extractor_RO_ComponentLookup;
            jobData.m_LandValues = __TypeHandle.__Game_Net_LandValue_RO_ComponentLookup;
            jobData.m_ConsumptionDatas = __TypeHandle.__Game_Prefabs_ConsumptionData_RO_ComponentLookup;
            jobData.m_Citizens = __TypeHandle.__Game_Citizens_HouseholdCitizen_RO_BufferLookup;
            jobData.m_ServiceCoverages = __TypeHandle.__Game_Net_ServiceCoverage_RO_BufferLookup;
            jobData.m_Availabilities = __TypeHandle.__Game_Net_ResourceAvailability_RO_BufferLookup;
            jobData.m_SubAreas = __TypeHandle.__Game_Areas_SubArea_RO_BufferLookup;
            jobData.m_SpawnableBuildings = __TypeHandle.__Game_Prefabs_SpawnableBuildingData_RO_ComponentLookup;
            jobData.m_ResourceDatas = __TypeHandle.__Game_Prefabs_ResourceData_RO_ComponentLookup;
            jobData.m_StorageCompanies = __TypeHandle.__Game_Companies_StorageCompany_RO_ComponentLookup;
            jobData.m_WorkplaceDatas = __TypeHandle.__Game_Prefabs_WorkplaceData_RO_ComponentLookup;
            jobData.m_Abandoned = __TypeHandle.__Game_Buildings_Abandoned_RO_ComponentLookup;
            jobData.m_Destroyed = __TypeHandle.__Game_Common_Destroyed_RO_ComponentLookup;
            jobData.m_Crimes = __TypeHandle.__Game_Buildings_CrimeProducer_RO_ComponentLookup;
            jobData.m_Locked = __TypeHandle.__Game_Prefabs_Locked_RO_ComponentLookup;
            jobData.m_Transforms = __TypeHandle.__Game_Objects_Transform_RO_ComponentLookup;
            jobData.m_CityModifiers = __TypeHandle.__Game_City_CityModifier_RO_BufferLookup;
            jobData.m_Districts = __TypeHandle.__Game_Areas_CurrentDistrict_RO_ComponentLookup;
            jobData.m_DistrictModifiers = __TypeHandle.__Game_Areas_DistrictModifier_RO_BufferLookup;
            jobData.m_HealthProblems = __TypeHandle.__Game_Citizens_HealthProblem_RO_ComponentLookup;
            jobData.m_Employees = __TypeHandle.__Game_Companies_Employee_RO_BufferLookup;
            jobData.m_BuildingEfficiencies = __TypeHandle.__Game_Buildings_Efficiency_RO_BufferLookup;
            jobData.m_ExtractorDatas = __TypeHandle.__Game_Prefabs_ExtractorAreaData_RO_ComponentLookup;
            jobData.m_CitizenDatas = __TypeHandle.__Game_Citizens_Citizen_RO_ComponentLookup;
            jobData.m_Students = __TypeHandle.__Game_Citizens_Student_RO_ComponentLookup;
            jobData.m_SpawnableBuildingData = __TypeHandle.__Game_Prefabs_SpawnableBuildingData_RO_ComponentLookup;
            jobData.m_ZoneData = __TypeHandle.__Game_Prefabs_ZoneData_RO_ComponentLookup;
            jobData.m_TradeCosts = __TypeHandle.__Game_Companies_TradeCost_RO_BufferLookup;
            jobData.m_BuildingNotifications = __TypeHandle.__Game_Buildings_BuildingNotifications_RW_ComponentLookup;
            jobData.m_ElectricityConsumers = __TypeHandle.__Game_Buildings_ElectricityConsumer_RO_ComponentLookup;
            jobData.m_AdjustHappinessDatas = __TypeHandle.__Game_Prefabs_AdjustHappinessData_RO_ComponentLookup;
            jobData.m_WaterConsumers = __TypeHandle.__Game_Buildings_WaterConsumer_RO_ComponentLookup;
            jobData.m_GarbageProducers = __TypeHandle.__Game_Buildings_GarbageProducer_RO_ComponentLookup;
            jobData.m_MailProducers = __TypeHandle.__Game_Buildings_MailProducer_RO_ComponentLookup;
            jobData.m_Abandoneds = __TypeHandle.__Game_Buildings_Abandoned_RO_ComponentLookup;
            jobData.m_ExtractorProperties = __TypeHandle.__Game_Buildings_ExtractorProperty_RO_ComponentLookup;
            jobData.m_Resources = __TypeHandle.__Game_Economy_Resources_RO_BufferLookup;
            jobData.m_HouseholdMembers = __TypeHandle.__Game_Citizens_HouseholdMember_RO_ComponentLookup;
            jobData.m_PollutionMap = m_GroundPollutionSystem.GetMap(readOnly: true, out var dependencies);
            jobData.m_AirPollutionMap = m_AirPollutionSystem.GetMap(readOnly: true, out var dependencies2);
            jobData.m_NoiseMap = m_NoisePollutionSystem.GetMap(readOnly: true, out var dependencies3);
            jobData.m_TelecomCoverages = m_TelecomCoverageSystem.GetData(readOnly: true, out var dependencies4);
            jobData.m_ExtractorParameters = m_ExtractorParameterQuery.GetSingleton<ExtractorParameterData>();
            jobData.m_HealthcareParameters = m_HealthcareParameterQuery.GetSingleton<HealthcareParameterData>();
            jobData.m_ParkParameters = m_ParkParameterQuery.GetSingleton<ParkParameterData>();
            jobData.m_EducationParameters = m_EducationParameterQuery.GetSingleton<EducationParameterData>();
            jobData.m_TelecomParameters = m_TelecomParameterQuery.GetSingleton<TelecomParameterData>();
            jobData.m_GarbageParameters = m_GarbageParameterQuery.GetSingleton<GarbageParameterData>();
            jobData.m_PoliceParameters = m_PoliceParameterQuery.GetSingleton<PoliceConfigurationData>();
            jobData.m_CitizenHappinessParameterData = m_CitizenHappinessParameterQuery.GetSingleton<CitizenHappinessParameterData>();
            jobData.m_BuildingConfigurationData = m_BuildingParameterQuery.GetSingleton<BuildingConfigurationData>();
            jobData.m_PollutionParameters = m_PollutionParameterQuery.GetSingleton<PollutionParameterData>();
            jobData.m_ResourcePrefabs = m_ResourceSystem.GetPrefabs();
            jobData.m_TaxRates = m_TaxSystem.GetTaxRates();
            jobData.m_Unemployment = m_CountEmploymentSystem.GetUnemploymentByEducation(out var deps);
            jobData.m_EconomyParameters = m_EconomyParameterQuery.GetSingleton<EconomyParameterData>();
            jobData.m_DemandParameters = m_DemandParameterQuery.GetSingleton<DemandParameterData>();
            jobData.m_BaseConsumptionSum = m_ResourceSystem.BaseConsumptionSum;
            jobData.m_City = m_CitySystem.City;
            jobData.m_UpdateFrameIndex = updateFrame;
            jobData.m_CommandBuffer = m_EndFrameBarrier.CreateCommandBuffer().AsParallelWriter();
            jobData.m_IconCommandBuffer = m_IconCommandSystem.CreateCommandBuffer();
            JobHandle jobHandle = JobChunkExtensions.ScheduleParallel(jobData, m_BuildingQuery, JobUtils.CombineDependencies(dependencies, dependencies2, dependencies3, dependencies4, deps, base.Dependency));
            m_EndFrameBarrier.AddJobHandleForProducer(jobHandle);
            m_ResourceSystem.AddPrefabsReader(jobHandle);
            m_GroundPollutionSystem.AddReader(jobHandle);
            m_AirPollutionSystem.AddReader(jobHandle);
            m_NoisePollutionSystem.AddReader(jobHandle);
            m_TelecomCoverageSystem.AddReader(jobHandle);
            m_CountEmploymentSystem.AddReader(jobHandle);
            m_TaxSystem.AddReader(jobHandle);
            m_IconCommandSystem.AddCommandBufferWriter(jobHandle);
            base.Dependency = jobHandle;
        }

        public static int2 GetRent(ConsumptionData consumptionData, BuildingPropertyData buildingProperties, float landValue, Game.Zones.AreaType areaType)
        {
            float2 @float = default(float2);
            @float.x = (float)consumptionData.m_Upkeep / PropertyRenterSystem.GetUpkeepExponent(areaType);
            @float.y = @float.x;
            float num = ((buildingProperties.m_ResidentialProperties <= 0 || (buildingProperties.m_AllowedSold == Resource.NoResource && buildingProperties.m_AllowedManufactured == Resource.NoResource)) ? ((float)buildingProperties.CountProperties()) : ((float)Mathf.RoundToInt((float)buildingProperties.m_ResidentialProperties / (1f - kMixedCompanyRent))));
            @float.x += math.max(0f, 1f * landValue);
            @float /= num;
            return new int2(Mathf.RoundToInt(@float.x), Mathf.RoundToInt(@float.y));
        }

        public static int2 CalculateMaximumRent(Entity renter, ref EconomyParameterData economyParameters, ref DemandParameterData demandParameters, float baseConsumptionSum, DynamicBuffer<CityModifier> cityModifiers, PropertyRenter propertyRenter, Entity healthcareService, Entity entertainmentService, Entity educationService, Entity telecomService, Entity garbageService, Entity policeService, ref ComponentLookup<Household> households, ref ComponentLookup<Worker> workers, ref ComponentLookup<Building> buildings, ref BufferLookup<HouseholdCitizen> householdCitizens, ref ComponentLookup<PrefabRef> prefabs, ref BufferLookup<ResourceAvailability> availabilities, ref ComponentLookup<BuildingPropertyData> buildingProperties, ref ComponentLookup<BuildingData> buildingDatas, ref ComponentLookup<SpawnableBuildingData> spawnableBuildings, ref ComponentLookup<CrimeProducer> crimes, ref BufferLookup<Game.Net.ServiceCoverage> serviceCoverages, ref ComponentLookup<Locked> locked, ref ComponentLookup<ElectricityConsumer> electricityConsumers, ref ComponentLookup<AdjustHappinessData> adjustHappinessDatas, ref ComponentLookup<WaterConsumer> waterConsumers, ref ComponentLookup<GarbageProducer> garbageProducers, ref ComponentLookup<MailProducer> mailProducers, ref ComponentLookup<Game.Objects.Transform> transforms, NativeArray<GroundPollution> pollutionMap, NativeArray<AirPollution> airPollutionMap, NativeArray<NoisePollution> noiseMap, CellMapData<TelecomCoverage> telecomCoverages, ResourcePrefabs resourcePrefabs, ref ComponentLookup<ResourceData> resourceDatas, ref ComponentLookup<IndustrialProcessData> processDatas, ref ComponentLookup<Game.Companies.StorageCompany> storageCompanies, ref ComponentLookup<ServiceAvailable> serviceAvailables, ref ComponentLookup<WorkProvider> workProviders, ref ComponentLookup<ServiceCompanyData> serviceCompanyDatas, ref ComponentLookup<WorkplaceData> workplaceDatas, ref ComponentLookup<Game.Companies.ProcessingCompany> processingCompanies, ref ComponentLookup<BuyingCompany> buyingCompanies, ref BufferLookup<Game.Areas.SubArea> subAreas, ref ComponentLookup<Attached> attached, ref ComponentLookup<Game.Areas.Lot> lots, ref ComponentLookup<Geometry> geometries, ref ComponentLookup<Extractor> areaExtractors, ref ComponentLookup<HealthProblem> healthProblems, CitizenHappinessParameterData happinessParameterData, GarbageParameterData garbageParameterData, NativeArray<int> taxRates, ref ComponentLookup<CurrentDistrict> districts, ref BufferLookup<DistrictModifier> districtModifiers, ref BufferLookup<Employee> employees, ref BufferLookup<Efficiency> buildingEfficiencies, ref ComponentLookup<ExtractorAreaData> extractorDatas, ExtractorParameterData extractorParameters, ref ComponentLookup<Citizen> citizenDatas, ref ComponentLookup<Game.Citizens.Student> students, NativeArray<int> unemployment, ref BufferLookup<TradeCost> tradeCosts, ref ComponentLookup<Abandoned> abandoneds)
        {
            Entity property = propertyRenter.m_Property;
            if (households.HasComponent(renter))
            {
                float commuteTime = 0f;
                if (workers.HasComponent(renter))
                {
                    commuteTime = workers[renter].m_LastCommuteTime;
                }
                Building buildingData = buildings[property];
                DynamicBuffer<HouseholdCitizen> dynamicBuffer = householdCitizens[renter];
                int length = dynamicBuffer.Length;
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                for (int i = 0; i < dynamicBuffer.Length; i++)
                {
                    Entity citizen = dynamicBuffer[i].m_Citizen;
                    Citizen citizen2 = citizenDatas[citizen];
                    num += (citizenDatas.HasComponent(citizen) ? citizenDatas[citizen].Happiness : 50);
                    if (citizen2.GetAge() == CitizenAge.Child)
                    {
                        num3++;
                    }
                    else
                    {
                        num2 += CitizenHappinessSystem.GetTaxBonuses(citizen2.GetEducationLevel(), taxRates, in happinessParameterData).y;
                    }
                }
                num /= math.max(1, dynamicBuffer.Length);
                num2 /= math.max(1, dynamicBuffer.Length - num3);
                Entity prefab = prefabs[property].m_Prefab;
                float shoppingTime = HouseholdFindPropertySystem.EstimateShoppingTime(buildingData.m_RoadEdge, buildingData.m_CurvePosition, hasCar: true, availabilities);
                float apartmentQuality = HouseholdFindPropertySystem.GetApartmentQuality(dynamicBuffer.Length, num3, property, ref buildingData, prefab, ref buildingProperties, ref buildingDatas, ref spawnableBuildings, ref crimes, ref serviceCoverages, ref locked, ref electricityConsumers, ref adjustHappinessDatas, ref waterConsumers, ref garbageProducers, ref mailProducers, ref prefabs, ref transforms, ref abandoneds, pollutionMap, airPollutionMap, noiseMap, telecomCoverages, cityModifiers, healthcareService, entertainmentService, educationService, telecomService, garbageService, policeService, happinessParameterData, garbageParameterData, num);
                int householdIncome = HouseholdBehaviorSystem.GetHouseholdIncome(dynamicBuffer, ref workers, ref citizenDatas, ref healthProblems, ref economyParameters, taxRates);
                int householdExpectedIncome = HouseholdBehaviorSystem.GetHouseholdExpectedIncome(dynamicBuffer, ref students, ref healthProblems, ref citizenDatas, ref economyParameters, taxRates, unemployment);
                int householdIncomeDefaultTax = HouseholdBehaviorSystem.GetHouseholdIncomeDefaultTax(dynamicBuffer, ref workers, ref healthProblems, ref citizenDatas, ref economyParameters);
                int householdExpectedIncomeDefault = HouseholdBehaviorSystem.GetHouseholdExpectedIncomeDefault(dynamicBuffer, ref students, ref healthProblems, ref citizenDatas, ref economyParameters);
                int highestEducation = HouseholdBehaviorSystem.GetHighestEducation(dynamicBuffer, ref citizenDatas);
                float3 res;
                float3 quality;
                int val = HouseholdFindPropertySystem.FindRentToProvideUtility(HouseholdFindPropertySystem.EvaluateDefaultProperty(householdIncomeDefaultTax, householdExpectedIncomeDefault, length, highestEducation, ref economyParameters, ref demandParameters, resourcePrefabs, resourceDatas, baseConsumptionSum, happinessParameterData, bonus: false, print: false, out res, out quality), length, householdIncome, householdExpectedIncome, commuteTime, shoppingTime, apartmentQuality + (float)num2 / 2f, ref economyParameters, resourcePrefabs, resourceDatas, baseConsumptionSum, happinessParameterData);
                int num4 = Math.Max(0, Math.Min(Mathf.RoundToInt(0.45f * (float)householdIncome), val));
                return new int2(num4, num4);
            }
            Entity prefab2 = prefabs[renter].m_Prefab;
            Entity prefab3 = prefabs[property].m_Prefab;
            IndustrialProcessData processData = processDatas[prefab2];
            int num5 = 0;
            int y = 0;
            float efficiency = BuildingUtils.GetEfficiency(property, ref buildingEfficiencies);
            if (storageCompanies.HasComponent(renter))
            {
                num5 = 0;
                y = 0;
            }
            else if (serviceAvailables.HasComponent(renter))
            {
                WorkProvider workProvider = workProviders[renter];
                ServiceAvailable service = serviceAvailables[renter];
                ServiceCompanyData serviceData = serviceCompanyDatas[prefab2];
                WorkplaceData workplaceData = workplaceDatas[prefab2];
                DynamicBuffer<Employee> employees2 = employees[renter];
                DynamicBuffer<TradeCost> tradeCosts2 = tradeCosts[renter];
                BuildingData buildingData2 = buildingDatas[prefab3];
                SpawnableBuildingData spawnableData = spawnableBuildings[prefab3];
                int fittingWorkers = CommercialAISystem.GetFittingWorkers(buildingDatas[prefab3], buildingProperties[prefab3], spawnableData.m_Level, serviceData);
                num5 = Mathf.RoundToInt(ServiceCompanySystem.EstimateDailyProfit(efficiency, workProvider.m_MaxWorkers, employees2, service, serviceData, buildingData2, processData, ref economyParameters, workplaceData, spawnableData, resourcePrefabs, resourceDatas, tradeCosts2));
                y = math.max(num5, Mathf.RoundToInt(ServiceCompanySystem.EstimateDailyProfitFull(1f, fittingWorkers, service, serviceData, buildingData2, processData, ref economyParameters, workplaceData, spawnableData, resourcePrefabs, resourceDatas, tradeCosts2)));
                int num6;
                if (districts.HasComponent(property))
                {
                    Entity district = districts[property].m_District;
                    num6 = TaxSystem.GetModifiedCommercialTaxRate(processData.m_Output.m_Resource, taxRates, district, districtModifiers);
                }
                else
                {
                    num6 = TaxSystem.GetCommercialTaxRate(processData.m_Output.m_Resource, taxRates);
                }
                num5 = Mathf.RoundToInt((float)num5 * (1f - (float)num6 / 100f));
                y = Mathf.RoundToInt((float)y * (1f - (float)num6 / 100f));
            }
            else if (processingCompanies.HasComponent(renter))
            {
                WorkProvider workProvider2 = workProviders[renter];
                if (buyingCompanies.HasComponent(renter) && tradeCosts.HasBuffer(renter))
                {
                    SpawnableBuildingData building = spawnableBuildings[prefab3];
                    DynamicBuffer<Employee> employees3 = employees[renter];
                    int fittingWorkers2 = IndustrialAISystem.GetFittingWorkers(buildingDatas[prefab3], buildingProperties[prefab3], building.m_Level, processData);
                    WorkplaceData workplaceData2 = workplaceDatas[prefab2];
                    num5 = Mathf.RoundToInt(ProcessingCompanySystem.EstimateDailyProfit(employees3, efficiency, workProvider2, processData, ref economyParameters, tradeCosts[renter], workplaceData2, building, resourcePrefabs, resourceDatas));
                    y = Mathf.RoundToInt(ProcessingCompanySystem.EstimateDailyProfitFull(1f, fittingWorkers2, processData, ref economyParameters, tradeCosts[renter], workplaceData2, building, resourcePrefabs, resourceDatas));
                    num5 = Mathf.RoundToInt((float)num5 * (1f - (float)TaxSystem.GetIndustrialTaxRate(processData.m_Output.m_Resource, taxRates) / 100f));
                    y = math.max(num5, Mathf.RoundToInt((float)y * (1f - (float)TaxSystem.GetIndustrialTaxRate(processData.m_Output.m_Resource, taxRates) / 100f)));
                }
                else if (attached.HasComponent(property))
                {
                    DynamicBuffer<Employee> employees4 = employees[renter];
                    WorkplaceData workplaceData3 = workplaceDatas[prefab2];
                    SpawnableBuildingData building2 = spawnableBuildings[prefab3];
                    num5 = ExtractorCompanySystem.EstimateDailyProfit(ExtractorCompanySystem.EstimateDailyProduction(efficiency, workProvider2.m_MaxWorkers, building2.m_Level, workplaceData3, processData, ref economyParameters), employees4, processData, ref economyParameters, workplaceData3, building2, resourcePrefabs, resourceDatas);
                    num5 = Mathf.RoundToInt((float)num5 * (1f - (float)TaxSystem.GetIndustrialTaxRate(processData.m_Output.m_Resource, taxRates) / 100f));
                    y = ExtractorCompanySystem.EstimateDailyProfitFull(ExtractorCompanySystem.EstimateDailyProduction(1f, workProvider2.m_MaxWorkers, building2.m_Level, workplaceData3, processData, ref economyParameters), processData, ref economyParameters, workplaceData3, building2, resourcePrefabs, resourceDatas);
                }
                else
                {
                    num5 = 0;
                }
            }
            num5 = Math.Max(0, num5);
            return new int2(num5, y);
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
        public RentAdjustSystem()
        {
        }
    }

}
