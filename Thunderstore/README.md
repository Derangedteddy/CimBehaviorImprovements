# Cities Skylines 2 : Cim Behavior Improvements Mod
Cim Behavior Improvement is a mod for Cities Skylines 2 that endeavors to improve the behavioral AI for Cims, aligning their choices more with reality to better simulate the behavior of humans IRL.  The goal is to reduce the number of residential high rent warnings in the city caused by Cims who have living situations that are generally unrealistic and aren't something that the player can fix.

> [!IMPORTANT]
> Cities Skylines 2 modding is in a volatile state right now since Colossal Order has not yet released official modding tools.  Download and install mods at your own risk, and only download mods from their official sources (GitHub, Thunderstore).  Many websites steal mods from official sources, manipulate them to add malware, and upload them to unofficial websites.  This mod and all others that I develop will ONLY be available here on GitHub and should be considered the only official source.

## Features
### Adjusts the chance that an **adult** Cim will apply to school in the following ways:
  - If the **adult** Cim has children, the chance of applying to school is divided by four.
### Adjusts the chance that a Cim will give birth to a child in the following ways:
  - If the Cim is >= 63 days of age, the chance of having a child is divided by four.
    - This reduces the number of senior Cims who have young children by preventing them from having kids too late in life. 
  - If the Cim's partner is >= 63 days of age, the normal boost to birth chance is not applied.
    - Normally, the game applies a bonus to the chance of birth if the Cim has an adult male partner.
  - If the Cim is a student, the chance of having a child is divided by four.
    - The game's default is to divide the chance by 2 for students.
  - If the Cim's household size is >= 4, the chance of having a child is divided by four.
    - This significantly reduces the chance that extremely large families will form, and keeps the household size reasonable.
### Student stipend
  - Students now receive a stipend of $1,000/month for housing costs.  Previously, they received the default unemployment benefit, which was too low to avoid high rent warnings in most cases.
  - This should be enough for most students to survive with a small household, but students with multiple children will still struggle.
### Supresses high rent warnings for households with significant savings
  - Suppresses high rent warnings for households that have more than $1,000 cash on hand.  These households are not in immediate jeopardy and may recover soon, so it doesn't make sense to show a warning for a household that can continue paying rent for several months or years.
  - Note: The dev tools do not show the amount of cash on hand for households.  The metrics it displays are different values that take more than just cash into consideration, such as the value of cars, other resources, etc.  I have verified the cash-on-hand calculation is accurate in the back-end.

## Requirements and Compatibility
- Cities Skylines II version 1.0.14f1 (November 16th 2023 update)
- BepInEx 5

## Installation
Place the `CimBehaviorImprovements.dll` file in your BepInEx `Plugins` folder.

## Known Issues
- [x] ~~Apply to school logic not working~~

## Changelog
- v0.0.1 (11/24/2023)
  - Initial alpha build.
- v.0.0.2 (11/24/2023)
  - Fixed ApplyToSchoolSystem logic so that it now successfully queries the household for dependent children and seniors.
- v.1.0.0 (11/25/2023)
  - Added student stipend
  - Added logic to suppress high rent warnings for households with more than $1,000 cash on hand.
- v.1.0.1 (11/25/2023)
  - Fixed an issue where student stipend was not restricted to adult students

## Planned Features
- User configurable options
  - Age at which birth chance penalties apply
  - Birth chance penalty for each scenario described in "Features"
  - Toggles to turn on/off adjustments for specific scenarios
  
## Disclaimers and Notes
> [!CAUTION]
> You are downloading, installing, and running this mod on your computer at your own risk.  I do not accept any responsibility for damage caused to your game, your computer, or your Cim's lives.

> [!IMPORTANT]
> The changes this mod makes take a significant amount of time to affect an existing city.  It may take many, **MANY** hours of gameplay before you notice any reduction in high rent warnings in a city that was built prior to installing this mod.  It is recommended that you start a new city to realize the most benefit in the shortest time possible.

> [!NOTE]
> This mod is not intended to reduce the number of seniors with teenage children.  Adults only have 63 days before they age up to seniors.  Children become teens after 21 days of age, and teens age up to adulthood after 36 days. Normally, adults have 63 days to have children.  This mod reduces that window to 42 days, and changing it to avoid seniors with teenage children would mean reducing the window even further to 27 days.  I fear that this would destroy the birth rate in cities, so I tried to strike a balance between realism and not significantly affecting the birth rate.  Future updates to the mod will allow players to tweak these values if they wish.

> [!NOTE]
> The original project name was "Responsible Cims" which I have since changed because I do not like the connotations that title has.  If you see references to "Responsible Cims," please understand that I do not like this title and do not wish for my mod to be known by it.  These are simply references to object titles that would take an enormous amount of time and effort to change.  I firmly reject the idea that everyone who finds themselves in situations such as the ones described in this mod made a conscious choice, and recognize that this was an error on my part to name the project in a way that suggests I believe otherwise.
