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
        // For some reason, the cloned TrafficLightInitializationSystem never receives calls to update. So we have to do it manually.
        // Could be something with the EntityQuery. I'm not able to find out the reason behind it.
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

    //[HarmonyPatch(typeof(Game.Simulation.ApplyToSchoolSystem), "OnUpdate")]
    //[HarmonyPrefix]
    //static bool OnUpdate(Game.Simulation.ApplyToSchoolSystem __instance)
    //{
    //    // For some reason, the cloned TrafficLightInitializationSystem never receives calls to update. So we have to do it manually.
    //    // Could be something with the EntityQuery. I'm not able to find out the reason behind it.
    //    __instance.World.GetOrCreateSystemManaged<ResponsibleCims.ApplyToSchoolSystem>().Update();
    //    return false;
    //}
}

