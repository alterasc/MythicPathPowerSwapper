# Mythic Path Power Swapper mod for Pathfinder: Wrath of the Righteous

Allows changing mythic powers to powers from another path, or playing without mythic powers at all.

## Download

I recommend using [Modfinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/releases) to download WotR mods. 

But if you don't want to, here are the links for manual download (**Install only one, not both**):  

First get [ModMenu](https://github.com/WittleWolfie/ModMenu/releases) if you don't have it

Now download this mod: [**LINK**](https://github.com/alterasc/CombatRelief/releases/latest)

## How to install:

1. Use Unity Mod Manager.
2. Install ModMenu.
3. Install this mod.


## Functionality

Does NOT introduce save dependency, so safe to remove mid-game. Just do respec and things should mostly (see notes below) return to normal.
Affects ALL saves globally.

*CHANGES APPLY ONLY AFTER RESTART*

How to use: 
Choose which mythic path to change.
Choose how to change.


Option "remove path powers" makes your progression like your companions. "Remove all powers" removes everything. You still has mythic rank, but no abilities.

So for example if you pick Demon in first selection and Lich in second, your Demon will have Lich powers and Lich spellbook.

Note on "Add Aivu": 
This adds Aivu to the selected mythic path (regardless of what you're copying from). I don't know what that will do. You wil have her, but will she talk, get recognized, break quests or your whole game - no clue. It's just a setting that took me 20 minutes to add, so I did because I can. If you try it, feel free to tell me how things went for you.

Options to demythic mythic hero and companions can be enabled without changes to your path powers.

## Notes
Affects ALL saves. Because it does not create new blueprints (for save safety ) it changes existing. As unfortunate side effect of this if you set for example Lich have Aeon powers, that changes some Aeon powers to rely on Lich Mythic class, meaning it can and will a bit mess things up if you load game where you're Aeon.

Affects things that are defined in mythic class progression. If you see it on mythic page - it's will be affected.   
You will get spellbook from the class that you copy from.    
You will receive Artifact Cloak of your original path, but enchantment will be swapped to the copied path.   
Story will proceed as your original path, there will be no recognition of "wrong" powers.    
If you're on Azata path you get Aivu as normal, because she's important to the story.   
If you copy from Azata, you by default do not gain Aivu, unless you toggle "Add Aivu" to On.

Azata receives spells depending on sub-path. So if you copy from Azata you don't get them. My advice - use TTT-Reworks, it adds those spells anyway.

Late game paths are unchanged. If you swap to Devil with changed progression I have no idea what will happen. Probably nothing, but it's on you.
Changing to other late game paths should work as usual.

## Troubleshooting
Unless you enabled "Add Aivu", turning mod off and doing respec will solve all your problems.
If you used "Add Aivu" maybe respec will help. Maybe not.

## Compatibility with mods
Maybe?   
Depends on what and how they do things. 
Is mostly compatible with TTT at least, that I checked.
I've tried to set priorities so it loads also after Path of Rage and Spellbook Merge, so maybe their changes are forwarded.


## More spellbook merge? 
NO.  
Not touching it.
