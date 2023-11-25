using BepInEx.Logging;
using HarmonyLib;

namespace ResponsibleCims;

[HarmonyPatch]
class Patches
{
    [HarmonyPatch(typeof(Game.Simulation.BirthSystem), "OnCreate")]
    [HarmonyPrefix]
    static bool OnCreate(Game.Simulation.BirthSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.BirthSystem>();
        __instance.World.GetOrCreateSystemManaged<Game.UpdateSystem>().UpdateAt<ResponsibleCims.BirthSystem>(Game.SystemUpdatePhase.GameSimulation);
        return true; // Allow the original method to run so that we only receive update requests when necessary
    }

    [HarmonyPatch(typeof(Game.Simulation.BirthSystem), "OnCreateForCompiler")]
    [HarmonyPrefix]
    static bool OnCreateForCompiler()
    {
        return false;
    }

    [HarmonyPatch(typeof(Game.Simulation.BirthSystem), "OnUpdate")]
    [HarmonyPrefix]
    static bool OnUpdate(Game.Simulation.BirthSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.BirthSystem>().Update();
        return false;
    }

    [HarmonyPatch(typeof(Game.Simulation.ApplyToSchoolSystem), "OnCreate")]
    [HarmonyPrefix]
    static bool OnCreate(Game.Simulation.ApplyToSchoolSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.ApplyToSchoolSystem>();
        __instance.World.GetOrCreateSystemManaged<Game.UpdateSystem>().UpdateAt<ResponsibleCims.ApplyToSchoolSystem>(Game.SystemUpdatePhase.GameSimulation);
        return true; // Allow the original method to run so that we only receive update requests when necessary
    }

    [HarmonyPatch(typeof(Game.Simulation.PayWageSystem), "OnCreate")]
    [HarmonyPrefix]
    static bool OnCreate(Game.Simulation.PayWageSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.PayWageSystem>();
        __instance.World.GetOrCreateSystemManaged<Game.UpdateSystem>().UpdateAt<ResponsibleCims.PayWageSystem>(Game.SystemUpdatePhase.GameSimulation);
        return true; // Allow the original method to run so that we only receive update requests when necessary
    }

    [HarmonyPatch(typeof(Game.Simulation.PayWageSystem), "OnUpdate")]
    [HarmonyPrefix]
    static bool OnUpdate(Game.Simulation.PayWageSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.PayWageSystem>().Update();
        return false;
    }

    [HarmonyPatch(typeof(Game.Simulation.RentAdjustSystem), "OnCreate")]
    [HarmonyPrefix]
    static bool OnCreate(Game.Simulation.RentAdjustSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.RentAdjustSystem>();
        __instance.World.GetOrCreateSystemManaged<Game.UpdateSystem>().UpdateAt<ResponsibleCims.RentAdjustSystem>(Game.SystemUpdatePhase.GameSimulation);
        return true; // Allow the original method to run so that we only receive update requests when necessary
    }

    [HarmonyPatch(typeof(Game.Simulation.RentAdjustSystem), "OnUpdate")]
    [HarmonyPrefix]
    static bool OnUpdate(Game.Simulation.RentAdjustSystem __instance)
    {
        __instance.World.GetOrCreateSystemManaged<ResponsibleCims.RentAdjustSystem>().Update();
        return false;
    }


}

