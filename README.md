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
4. Set settings to your liking.
5. Restart to apply them.
## Functionality

Does NOT introduce save dependency, so safe to remove mid-game. Just do respec and things should mostly (see notes below) return to normal.
Affects ALL saves globally.

**SETTING CHANGES APPLY ONLY AFTER RESTART**

How to use: 
Choose which mythic path to change.
Choose how to change.


Legendarify option remove mythic powers and spellbook, grants rising bonus to all stats and modifies XP table so you get levels earlier. See section later.

Option "remove path powers" makes your progression like your companions. "Remove all powers" removes everything. You still has mythic rank, but no abilities.

So for example if you pick Demon in first selection and Lich in second, your Demon will have Lich powers and Lich spellbook.

Note on "Add Aivu":  
This adds Aivu to the selected mythic path (regardless of what you're copying from). I don't know what that will do. You wil have her, but will she talk, get recognized, break quests or your whole game - no clue. It's just a setting that took me 20 minutes to add, so I did because I can. If you try it, feel free to tell me how things went for you.

Options to demythic mythic hero and companions can be enabled without changes to your path powers.

## Notes
- Affects ALL saves. Because it does not create new blueprints (for save safety ) it changes existing. As unfortunate side effect of this if you set for example Lich have Aeon powers, that changes some Aeon powers to rely on Lich Mythic class, meaning some Aeon powers will not work correctly when you're an Aeon until you reset the power swap or remove the mod.
- Affects things that are defined in mythic class progression. If you see it on mythic page - it's will be affected.   
- You will get spellbook from the class that you copy from.    
- You will receive Artifact Cloak of your original path, but enchantment will be swapped to the copied path.   
- Story will proceed as your original path, there will be no recognition of "wrong" powers.    
- If you're on Azata path you get Aivu as normal, because she's important to the story.   
- If you copy from Azata, you by default do not gain Aivu, unless you toggle "Add Aivu" to On.

- Azata receives spells depending on sub-path. So if you copy from Azata you don't get them. My advice - use TTT-Reworks, it adds those spells anyway.

- Late game paths are unchanged. If you swap to Devil with changed progression I have no idea what will happen. Probably nothing, but it's on you.
Changing to other late game paths that remove your old path abilities should work as usual.

## Legendarify
- Removes mythic powers
- On ranks 3-5-7-9 you get stacking +1 bonus to all stats (total +4 to all stats at MR9)
- With each mythic rank XP table gets shorter and shorter. At M3-M4 you can expect to be 1-2 levels higher than normal, at M5-M6 3-5 levels higher. At M9 progression is almost like normal legend, but to get to level 40 you need more experience than normal Legend. But you still will get lvl 40 eventually, just somewhat later. For comparison normal legend lvl 40 = legendarified lvl 38.
- If you swap to late game non-Legend path, you get to keep your levels over 20. As far as I'm concerned this is your responsibility.
- Exact XP table is at the bottom of readme.
- You also get hidden legend feature that removes path visual changes.

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

## Can I have multiple paths?  
Out of scope for this mod.


## Appendix

### XP Tables

Normal is normal XP table   
Legend is Legend XP table   
M3 - M9 is how your table changes over the course of mythic ranks.  

| Level | Normal | M3 | M4 | M5 | M6 | M7 | M8 | M9 | Legend |   
| - | - | - | - | - | - | - | - | - | - |   
| 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0
| 2 | 2000 | 2000 | 2000 | 2000 | 2000 | 2000 | 2000 | 2000 | 2000
| 3 | 5000 | 5000 | 5000 | 5000 | 5000 | 5000 | 5000 | 5000 | 5000
| 4 | 9000 | 9000 | 9000 | 9000 | 9000 | 9000 | 9000 | 9000 | 9000
| 5 | 15000 | 15000 | 15000 | 15000 | 15000 | 15000 | 15000 | 15000 | 15000
| 6 | 23000 | 23000 | 23000 | 23000 | 23000 | 23000 | 23000 | 23000 | 23000
| 7 | 35000 | 35000 | 35000 | 35000 | 35000 | 35000 | 35000 | 35000 | 35000
| 8 | 51000 | 51000 | 51000 | 51000 | 51000 | 51000 | 51000 | 51000 | 51000
| 9 | 75000 | 69000 | 67000 | 66000 | 64000 | 62000 | 61000 | 59000 | 55000
| 10 | 105000 | 94000 | 89000 | 85000 | 80000 | 77000 | 73000 | 69000 | 62000
| 11 | 155000 | 125000 | 120000 | 110000 | 100000 | 94000 | 87000 | 81000 | 68000
| 12 | 220000 | 170000 | 155000 | 140000 | 125000 | 115000 | 105000 | 94000 | 75000
| 13 | 315000 | 235000 | 205000 | 180000 | 160000 | 140000 | 125000 | 110000 | 85000
| 14 | 445000 | 315000 | 270000 | 235000 | 200000 | 170000 | 150000 | 125000 | 96000
| 15 | 635000 | 430000 | 360000 | 300000 | 250000 | 210000 | 175000 | 150000 | 105000
| 16 | 890000 | 580000 | 475000 | 385000 | 315000 | 260000 | 210000 | 170000 | 115000
| 17 | 1300000 | 785000 | 625000 | 500000 | 395000 | 315000 | 250000 | 200000 | 130000
| 18 | 1800000 | 1050000 | 830000 | 645000 | 500000 | 390000 | 300000 | 235000 | 155000
| 19 | 2550000 | 1450000 | 1100000 | 830000 | 625000 | 475000 | 360000 | 270000 | 180000
| 20 | 3600000 | 1950000 | 1450000 | 1050000 | 790000 | 580000 | 430000 | 315000 | 200000
| 21 |  | 2650000 | 1900000 | 1400000 | 990000 | 710000 | 515000 | 370000 | 220000
| 22 |  | 3600000 | 2550000 | 1750000 | 1250000 | 875000 | 610000 | 430000 | 260000
| 23 |  | 4900000 | 3350000 | 2300000 | 1550000 | 1050000 | 730000 | 500000 | 280000
| 24 |  | 6600000 | 4400000 | 2950000 | 1950000 | 1300000 | 875000 | 580000 | 315000
| 25 |  | 8950000 | 5850000 | 3800000 | 2450000 | 1600000 | 1050000 | 680000 | 370000
| 26 |  | 12000000 | 7700000 | 4900000 | 3100000 | 1950000 | 1250000 | 790000 | 445000
| 27 |  | 16500000 | 10000000 | 6300000 | 3900000 | 2400000 | 1500000 | 920000 | 500000
| 28 |  | 22500000 | 13500000 | 8100000 | 4900000 | 2950000 | 1800000 | 1050000 | 635000
| 29 |  | 30500000 | 18000000 | 10500000 | 6150000 | 3600000 | 2100000 | 1250000 | 720000
| 30 |  | 41000000 | 23500000 | 13500000 | 7700000 | 4400000 | 2550000 | 1450000 | 890000
| 31 |  | 55500000 | 31000000 | 17500000 | 9700000 | 5400000 | 3000000 | 1700000 | 1000000
| 32 |  | 75500000 | 41000000 | 22500000 | 12000000 | 6650000 | 3600000 | 1950000 | 1300000
| 33 |  | 100000000 | 54000000 | 29000000 | 15500000 | 8100000 | 4300000 | 2300000 | 1550000
| 34 |  | 140000000 | 71500000 | 37000000 | 19000000 | 9950000 | 5150000 | 2650000 | 1800000
| 35 |  | 190000000 | 94500000 | 48000000 | 24000000 | 12000000 | 6150000 | 3100000 | 2000000
| 36 |  | 255000000 | 125000000 | 61500000 | 30500000 | 15000000 | 7350000 | 3600000 | 2550000
| 37 |  | 345000000 | 165000000 | 79500000 | 38000000 | 18500000 | 8750000 | 4200000 | 3000000
| 38 |  | 465000000 | 220000000 | 100000000 | 48000000 | 22500000 | 10500000 | 4900000 | 3600000
| 39 |  | 635000000 | 290000000 | 130000000 | 60000000 | 27500000 | 12500000 | 5700000 | 4050000
| 40 |  | 860000000 | 380000000 | 170000000 | 75500000 | 33500000 | 15000000 | 6650000 | 4700000