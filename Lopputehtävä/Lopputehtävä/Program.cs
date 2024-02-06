using System;

// Create an instance off all weapons
WoodenSword woodenSword = new WoodenSword();
SteelSword steelSword = new SteelSword();
FireSword fireSword = new FireSword();

// Create an instance off all armor
LeatherArmor leatherArmor = new LeatherArmor();
BronzeArmor bronzeArmor = new BronzeArmor();
IronArmor ironArmor = new IronArmor();

// Create an instance off all potions
HealingPotionWeak weakHealingPotion = new HealingPotionWeak();
HealingPotionMedium mediumHealingPotion = new HealingPotionMedium();
HealingPotionStrong strongHealingPotion = new HealingPotionStrong();

// Create an instance of all enemies
Dragon dragon = new Dragon();
Golem golem = new Golem();
Goblin goblin = new Goblin();

// Create an instance of the player
Player player = new Player(100, 5000, woodenSword, leatherArmor);

// Start the main loop of the game
MainLoop();

// Main menu of the game
void MainLoop()
{
    //Empty console before menu
    Clear();

    player.CurrentHealth = player.MaxHealt;

    if (player.GoldAmount < 0)
    {
        player.GoldAmount = 0;
    }

    //Display the current status of the player
    SayLine("You're a brave warrior seeking for adventure", ConsoleColor.Yellow);
    Break();
    Say($"You're equipped with a ", ConsoleColor.Yellow);
    Say($"{player.ActiveWeapon.Name} ", ConsoleColor.DarkYellow);
    Say($"and ", ConsoleColor.Yellow);
    SayLine(player.ActiveArmor.Name, ConsoleColor.DarkYellow);
    Break();
    SayLine($"You have {player.GoldAmount} gold", ConsoleColor.DarkYellow);
    Break();
    SayLine("Do you wish to visit the shop for new equipment or do you want to continue your journey and see what lies ahead?", ConsoleColor.Magenta);
    SayLine("   1. Visit the shop", ConsoleColor.Cyan);
    SayLine("   2. Continue journey", ConsoleColor.Cyan);
    SayLine("   3. View backpack", ConsoleColor.Cyan);
    string selection = Listen();

    //Execute function based on input
    switch (selection)
    {
        case "1":
            VisitShop();
            break;
        case "2":
            ContinueJourney();
            break;
        case "3":
            ViewBackpack();
            break;
        default:
            PrintError(selection);
            MainLoop();
            break;
    }
}

void ContinueJourney()
{
    //Generate random number (1 in 5)
    Random rnd = new Random();
    int odds = rnd.Next(1, 6);

    //Finds chest
    if (odds == 1)
    {
        //Generate random amount of coins (10-50)
        int goldAmount = rnd.Next(1, 6);
        goldAmount *= 10;

        //Add gold to player and display coins found
        player.AddGold(goldAmount);

        Clear();

        //Display chest found text and amount of gold found
        SayLine("As you continue your journey, you spot something sticking out from the ground ahead", ConsoleColor.Yellow);
        SayLine("As you approach, you realize it's a chest! What treasures could be waiting inside?", ConsoleColor.Yellow);
        Break();
        SayLine($"You found {goldAmount} gold in a chest", ConsoleColor.Green);
        Break();

        Pause();
    }

    //Face an enemy
    else
    {
        BattleInit();
    }

    MainLoop();
}

void VisitShop()
{
    Clear();

    //Display welcoming text for the player and ask if player wants to buy or sell weapons
    SayLine("Greetings warrior!", ConsoleColor.Yellow);
    SayLine("I am Duke, the owner of this humble shop", ConsoleColor.Yellow);
    SayLine("If you're trying to buy or sell weapons, armor, and potions", ConsoleColor.Yellow);
    SayLine("You've come to the right place", ConsoleColor.Yellow);
    Break();
    SayLine("Buy equipment", ConsoleColor.Magenta);
    SayLine("   1. Weapons", ConsoleColor.Cyan);
    SayLine("   2. Armor", ConsoleColor.Cyan);
    SayLine("   3. Potions", ConsoleColor.Cyan);
    Break();
    SayLine("Sell equipment", ConsoleColor.Magenta);
    SayLine("   4. Weapons", ConsoleColor.Cyan);
    SayLine("   5. Armor", ConsoleColor.Cyan);
    SayLine("   6. Potions", ConsoleColor.Cyan);
    Break();
    SayLine("   9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    //Execute different buy menus based on input
    switch (decision)
    {
        case "1":
            ShopWeapons();
            break;
        case "2":
            ShopArmor();
            break;
        case "3":
            ShopPotions();
            break;
        case "4":
            SellWeapon();
            break;
        case "5":
            SellArmor();
            break;
        case "6":
            SellPotion();
            break;
        case "9":
            MainLoop();
            break;
        default:
            PrintError(decision);
            Break();
            VisitShop();
            break;
    }
}

void ShopWeapons()
{
    Clear();

    //Display weapon section text and ask for input
    SayLine("When it comes to weapons, the right choice can make all the difference in battle", ConsoleColor.Yellow);
    SayLine("My shop offers a diverse selection of weapons, each with their own strengths and weaknesses.", ConsoleColor.Yellow);
    Break();
    SayLine($"You have: {player.GoldAmount} gold", ConsoleColor.DarkYellow);
    Break();

    SayLine("Which weapon would you like to buy", ConsoleColor.Magenta);

    Say($"  1. {woodenSword.Name} ", ConsoleColor.Cyan);
    Say($"Cost {woodenSword.Cost} ", ConsoleColor.DarkYellow);
    SayLine(woodenSword.IsOwned(), ConsoleColor.Green);
    woodenSword.DisplayWeaponStats(true);
    Break();

    Say($"  2. {steelSword.Name} ", ConsoleColor.Cyan);
    Say($"Cost {steelSword.Cost} ", ConsoleColor.DarkYellow);
    SayLine(steelSword.IsOwned(), ConsoleColor.Green);
    steelSword.DisplayWeaponStats(true);
    Break();

    Say($"  3. {fireSword.Name} ", ConsoleColor.Cyan);
    Say($"Cost {fireSword.Cost} ", ConsoleColor.DarkYellow);
    SayLine(fireSword.IsOwned(), ConsoleColor.Green);
    fireSword.DisplayWeaponStats(true);
    Break();

    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    //Buy a sword based on input
    switch (decision)
    {
        case "1":
            woodenSword.Buy(player, woodenSword);
            break;
        case "2":
            steelSword.Buy(player, steelSword);
            break;
        case "3":
            fireSword.Buy(player, fireSword);
            break;
        case "9":
            VisitShop();
            break;
        default:
            PrintError(decision);
            break;
    }

    ShopWeapons();
}

void ShopArmor()
{
    Clear();

    //Display armor section text and ask for input
    SayLine("When it comes to protection in battle, there's no substitute for quality armor", ConsoleColor.Yellow);
    SayLine("Here in my shop, I offer a wide variety of armor types to suit any warrior's needs.", ConsoleColor.Yellow);
    Break();
    SayLine($"You have: {player.GoldAmount} gold", ConsoleColor.DarkYellow);
    Break();
    SayLine("Which armor would you like to buy", ConsoleColor.Magenta);

    Say($"  1. {leatherArmor.Name} ", ConsoleColor.Cyan);
    Say($"Cost {leatherArmor.Cost} ", ConsoleColor.DarkYellow);
    SayLine(leatherArmor.IsOwned(), ConsoleColor.Green);
    leatherArmor.DisplayArmorStats(true);
    Break();

    Say($"  2. {bronzeArmor.Name} ", ConsoleColor.Cyan);
    Say($"Cost {bronzeArmor.Cost} ", ConsoleColor.DarkYellow);
    SayLine(bronzeArmor.IsOwned(), ConsoleColor.Green);
    bronzeArmor.DisplayArmorStats(true);
    Break();

    Say($"  3. {ironArmor.Name} ", ConsoleColor.Cyan);
    Say($"Cost {ironArmor.Cost} ", ConsoleColor.DarkYellow);
    SayLine(ironArmor.IsOwned(), ConsoleColor.Green);
    ironArmor.DisplayArmorStats(true);
    Break();

    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    //Buy armor based on input
    switch (decision)
    {
        case "1":
            leatherArmor.Buy(player, leatherArmor);
            break;
        case "2":
            bronzeArmor.Buy(player, bronzeArmor);
            break;
        case "3":
            ironArmor.Buy(player, ironArmor);
            break;
        case "9":
            VisitShop();
            break;
        default:
            PrintError(decision);
            break;
    }

    ShopArmor();
}

void ShopPotions()
{
    Clear();

    //Display potion section text and ask for input
    SayLine("In battle, wounds are inevitable, and a good healing potion can be a lifesaver", ConsoleColor.Yellow);
    SayLine("Here in my shop, I offer a range of healing potions, each with its own unique strength", ConsoleColor.Yellow);
    Break();
    SayLine($"You have: {player.GoldAmount} gold", ConsoleColor.DarkYellow);
    Break();

    SayLine("Which potion would you like to buy", ConsoleColor.Magenta);

    Say($"  1. {weakHealingPotion.Name} ", ConsoleColor.Cyan);
    Say($"Cost {weakHealingPotion.Cost} ", ConsoleColor.DarkYellow);
    SayLine(weakHealingPotion.IsOwned(), ConsoleColor.Green);

    Say($"  2. {mediumHealingPotion.Name} ", ConsoleColor.Cyan);
    Say($"Cost {mediumHealingPotion.Cost} ", ConsoleColor.DarkYellow);
    SayLine(mediumHealingPotion.IsOwned(), ConsoleColor.Green);

    Say($"  3. {strongHealingPotion.Name} ", ConsoleColor.Cyan);
    Say($"Cost {strongHealingPotion.Cost} ", ConsoleColor.DarkYellow);
    SayLine(strongHealingPotion.IsOwned(), ConsoleColor.Green);

    Break();
    SayLine("9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    //Buy potion based on input
    switch (decision)
    {
        case "1":
            weakHealingPotion.Buy(player, weakHealingPotion);
            break;
        case "2":
            mediumHealingPotion.Buy(player, mediumHealingPotion);
            break;
        case "3":
            strongHealingPotion.Buy(player, strongHealingPotion);
            break;
        case "9":
            VisitShop();
            break;
        default:
            PrintError(decision);
            break;
    }

    ShopPotions();
}

void ViewBackpack()
{
    Clear();

    //Check if weaponList is empty
    if (player.weaponList.Count != 0)
    {
        //Display weapons in weaponList
        SayLine("Your weapons are:", ConsoleColor.Yellow);
        for (int i = 0; i < player.weaponList.Count; i++)
        {
            if (player.weaponList[i].isEquipped)
            {
                Say($"  {i + 1}. {player.weaponList[i].Name} ", ConsoleColor.DarkYellow);
                SayLine("Equipped", ConsoleColor.Green);
                player.weaponList[i].DisplayWeaponStats(true);
                Break();
            }
            else
            {
                SayLine($"  {i + 1}. {player.weaponList[i].Name}", ConsoleColor.DarkYellow);
                player.weaponList[i].DisplayWeaponStats(true);
                Break();
            }
        }
        Break();
    }
    else
    {
        //Display no weapons
        SayLine("You dont have any weapons", ConsoleColor.Red);
        Break();
    }

    //Check if armorList is empty
    if (player.armorList.Count != 0)
    {
        //Display armor in armorList
        SayLine("Your armor are:", ConsoleColor.Yellow);
        for (int i = 0; i < player.armorList.Count; i++)
        {
            if (player.armorList[i].isEquipped)
            {
                Say($"  {i + 1}. {player.armorList[i].Name} ", ConsoleColor.DarkYellow);
                SayLine("Equipped", ConsoleColor.Green);
                player.armorList[i].DisplayArmorStats(true);
                Break();
            }
            else
            {
                SayLine($"  {i + 1}. {player.armorList[i].Name}", ConsoleColor.DarkYellow);
                player.armorList[i].DisplayArmorStats(true);
                Break();
            }
        }
        Break();
    }
    else
    {
        //Display no armor
        SayLine("You dont have any armor", ConsoleColor.Red);
        Break();
    }

    //Check if potionList  is empty
    if (player.potionList.Count != 0)
    {
        //Display potions in potionList
        SayLine("Your potions are:", ConsoleColor.Yellow);
        for (int i = 0; i < player.potionList.Count; i++)
        {
            SayLine($"  {i + 1}. {player.potionList[i].Name}", ConsoleColor.DarkYellow);
        }
        Break();
    }
    else
    {
        //Display no potions
        SayLine("You dont have any potions", ConsoleColor.Red);
        Break();
    }

    //Ask for player input
    SayLine("   1. Equip weapons", ConsoleColor.Cyan);
    SayLine("   2. Equip armor", ConsoleColor.Cyan);
    Break();
    SayLine("   9. Go Back", ConsoleColor.Blue);
    string decision = Listen();

    //Execute equip menu based on input
    switch (decision)
    {
        case "1":
            EquipWeapon();
            break;
        case "2":
            EquipArmor();
            break;
        case "9":
            MainLoop();
            break;
        default:
            PrintError(decision);
            ViewBackpack();
            break;
    }
}

void SellWeapon()
{
    Clear();

    SayLine("Which weapon would you like to sell", ConsoleColor.Magenta);
    for (int i = 0; i < player.weaponList.Count; i++)
    {
        if (player.weaponList[i].isEquipped)
        {
            Say($"  {i + 1}. {player.weaponList[i].Name} ", ConsoleColor.Cyan);
            SayLine("Equipped", ConsoleColor.Green);
            player.weaponList[i].DisplayWeaponStats(true);
            Break();
        }
        else
        {
            SayLine($"  {i + 1}. {player.weaponList[i].Name}", ConsoleColor.Cyan);
            player.weaponList[i].DisplayWeaponStats(true);
            Break();
        }
    }
    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    if (player.weaponList.Count != 0)
    {
        bool foundItem = false;
        for (int i = 0; i < player.weaponList.Count; i++)
        {
            if (decision == (i + 1).ToString())
            {
                foundItem = true;
                if (player.weaponList[i].isEquipped)
                {
                    SayLine("Cannot sell equipped weapon", ConsoleColor.Red);
                    Pause();
                    SellWeapon();
                }
                else
                {
                    player.weaponList[i].Sell(player, player.weaponList[i]);
                    VisitShop();
                    break;
                }
            }
            else if (decision == "9")
            {
                foundItem = true;
                VisitShop();
                break;
            }
        }
        if (!foundItem)
        {
            PrintError(decision);
        }
        SellWeapon();
    }
    else
    {
        if (decision == "9")
        {
            VisitShop();
        }
        else
        {
            PrintError(decision);
        }
        SellWeapon();
    }
}

void SellArmor()
{
    Clear();

    SayLine("Which armor would you like to sell", ConsoleColor.Magenta);

    for (int i = 0; i < player.armorList.Count; i++)
    {
        if (player.armorList[i].isEquipped)
        {
            Say($"  {i + 1}. {player.armorList[i].Name} ", ConsoleColor.Cyan);
            SayLine("Equipped", ConsoleColor.Green);
            player.armorList[i].DisplayArmorStats(true);
            Break();
        }
        else
        {
            SayLine($"  {i + 1}. {player.armorList[i].Name}", ConsoleColor.Cyan);
            player.armorList[i].DisplayArmorStats(true);
            Break();
        }
    }
    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    if (player.armorList.Count != 0)
    {
        bool foundItem = false;
        for (int i = 0; i < player.armorList.Count; i++)
        {
            if (decision == (i + 1).ToString())
            {
                foundItem = true;
                if (player.armorList[i].isEquipped)
                {
                    SayLine("Cannot sell equipped armor", ConsoleColor.Red);
                    Pause();
                    SellArmor();
                }
                else
                {
                    player.armorList[i].Sell(player, player.armorList[i]);
                    VisitShop();
                    break;
                }
            }
            else if (decision == "9")
            {
                foundItem = true;
                VisitShop();
                break;
            }
        }
        if (!foundItem)
        {
            PrintError(decision);
        }
        SellArmor();
    }
    else
    {
        if (decision == "9")
        {
            VisitShop();
        }
        else
        {
            PrintError(decision);
        }
        SellArmor();
    }
}

void SellPotion()
{
    Clear();

    if (player.potionList.Count != 0)
    {
        SayLine("Which potion would you like to sell", ConsoleColor.Magenta);

        for (int i = 0; i < player.potionList.Count; i++)
        {
            SayLine($"  {i + 1}. {player.potionList[i].Name}", ConsoleColor.Cyan);
        }
        Break();
        SayLine("  9. Go back", ConsoleColor.Blue);
        string decision = Listen();

        if (player.potionList.Count != 0)
        {
            bool foundItem = false;
            for (int i = 0; i < player.potionList.Count; i++)
            {
                if (decision == (i + 1).ToString())
                {
                    foundItem = true;
                    player.potionList[i].Sell(player, player.potionList[i]);
                    VisitShop();
                    break;
                }
                else if (decision == "9")
                {
                    foundItem = true;
                    VisitShop();
                    break;
                }
            }
            if (!foundItem)
            {
                PrintError(decision);
            }
            SellPotion();
        }
        else
        {
            if (decision == "9")
            {
                VisitShop();
            }
            else
            {
                PrintError(decision);
            }
            SellPotion();
        }
    }
    else
    {
        SayLine("You dont have any potions to sell", ConsoleColor.Red);
        Pause();
        VisitShop();
    }
}

void EquipWeapon()
{
    Clear();

    SayLine("Which weapon would you like to equip", ConsoleColor.Magenta);

    for (int i = 0; i < player.weaponList.Count; i++)
    {
        if (player.weaponList[i].isEquipped)
        {
            Say($"  {i + 1}. {player.weaponList[i].Name} ", ConsoleColor.Cyan);
            SayLine("Equipped", ConsoleColor.Green);
            player.weaponList[i].DisplayWeaponStats(true);
            Break();
        }
        else
        {
            SayLine($"  {i + 1}. {player.weaponList[i].Name}", ConsoleColor.Cyan);
            player.weaponList[i].DisplayWeaponStats(true);
            Break();
        }
    }
    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    if (player.weaponList.Count != 0)
    {
        bool foundItem = false;
        for (int i = 0; i < player.weaponList.Count; i++)
        {

            if (decision == (i + 1).ToString())
            {
                foundItem = true;
                if (player.weaponList[i].isEquipped)
                {
                    SayLine($"{player.weaponList[i].Name} already equipped", ConsoleColor.Red);
                    Pause();
                    EquipWeapon();
                }
                else
                {
                    player.ActiveWeapon.isEquipped = false;
                    player.SetActiveWeapon(player.weaponList[i]);
                    ViewBackpack();
                    break;
                }
            }
            else if (decision == "9")
            {
                foundItem = true;
                ViewBackpack();
                break;
            }
        }
        if (!foundItem)
        {
            PrintError(decision);
        }
        EquipWeapon();
    }
    else
    {
        if (decision == "9")
        {
            ViewBackpack();
        }
        else
        {
            PrintError(decision);
        }
        EquipWeapon();
    }
}

void EquipArmor()
{
    Clear();

    SayLine("Which armor would you like to equip", ConsoleColor.Magenta);

    for (int i = 0; i < player.armorList.Count; i++)
    {
        if (player.armorList[i].isEquipped)
        {
            Say($"  {i + 1}. {player.armorList[i].Name} ", ConsoleColor.Cyan);
            SayLine("Equipped", ConsoleColor.Green);
            player.armorList[i].DisplayArmorStats(true);
            Break();
        }
        else
        {
            SayLine($"  {i + 1}. {player.armorList[i].Name}", ConsoleColor.Cyan);
            player.armorList[i].DisplayArmorStats(true);
            Break();
        }
    }
    Break();
    SayLine("  9. Go back", ConsoleColor.Blue);
    string decision = Listen();

    if (player.armorList.Count != 0)
    {
        bool foundItem = false;
        for (int i = 0; i < player.armorList.Count; i++)
        {

            if (decision == (i + 1).ToString())
            {
                foundItem = true;
                if (player.armorList[i].isEquipped)
                {
                    SayLine($"{player.armorList[i].Name} already equipped", ConsoleColor.Red);
                    Pause();
                    EquipArmor();
                }
                else
                {
                    player.ActiveArmor.isEquipped = false;
                    player.SetActiveArmor(player.armorList[i]);
                    ViewBackpack();
                    break;
                }
            }
            else if (decision == "9")
            {
                foundItem = true;
                ViewBackpack();
                break;
            }
        }
        if (!foundItem)
        {
            PrintError(decision);
        }
        EquipArmor();
    }
    else
    {
        if (decision == "9")
        {
            ViewBackpack();
        }
        else
        {
            PrintError(decision);
        }
        EquipWeapon();
    }
}

void BattleInit()
{
    Clear();

    //Generate random odds (1 in 3)
    Random rnd = new Random();
    int odds = rnd.Next(1, 3);

    Enemy selectedEnemy;

    switch (odds)
    {
        case 1:
            selectedEnemy = dragon;
            break;
        case 2:
            selectedEnemy = golem;
            break;
        default:
            selectedEnemy = goblin;
            break;
    }

    SayLine($"You faced a {selectedEnemy.Name}", ConsoleColor.Yellow);
    Pause();
    Battleloop(selectedEnemy);
}

void Battleloop(Enemy enemy)
{
    Clear();

    if (enemy.CurrentHealth <= 0)
    {
        SayLine($"You have won the battle and earned {enemy.RewardAmount} gold", ConsoleColor.DarkYellow);
        player.GoldAmount += enemy.RewardAmount;
        Pause();
        MainLoop();
    }
    else if (player.CurrentHealth <= 0)
    {
        SayLine($"You have been defeated, you lost {enemy.RewardAmount} gold", ConsoleColor.DarkYellow);
        player.GoldAmount -= enemy.RewardAmount;
        Pause();
        MainLoop();
    }
    else
    {
        SayLine($"Your hp: {player.CurrentHealth}/{player.MaxHealt}", ConsoleColor.Yellow);
        SayLine($"Enemy hp: {enemy.CurrentHealth}/{enemy.MaxHealth}", ConsoleColor.Red);
        Break();
        SayLine("What would you like to do", ConsoleColor.Magenta);
        SayLine("   1. Attack", ConsoleColor.Cyan);
        SayLine("   2. Drink potion", ConsoleColor.Cyan);
        SayLine("   3. Try to escape", ConsoleColor.Cyan);
        string decision = Listen();

        switch (decision)
        {
            case "1":
                SayLine($"You attacked the enemy and dealt {player.ActiveWeapon.Damage} to {enemy.Name}", ConsoleColor.DarkYellow);
                enemy.CurrentHealth -= player.ActiveWeapon.Damage;
                Pause();
                EnemyAttack(enemy);
                break;
            case "2":
                Clear();

                if (player.potionList.Count != 0)
                {
                    SayLine("Which potion would you like to use", ConsoleColor.Magenta);

                    for (int i = 0; i < player.potionList.Count; i++)
                    {
                        SayLine($"  {i + 1}. {player.potionList[i].Name}", ConsoleColor.Cyan);
                    }
                    Break();
                    SayLine("  9. Go back", ConsoleColor.Blue);
                    string decision1 = Listen();

                    if (player.potionList.Count != 0)
                    {
                        bool foundItem = false;
                        for (int i = 0; i < player.potionList.Count; i++)
                        {
                            if (decision1 == (i + 1).ToString())
                            {
                                foundItem = true;
                                player.potionList[i].Consume(player, player.potionList[i]);
                                Battleloop(enemy);
                                break;
                            }
                            else if (decision1 == "9")
                            {
                                foundItem = true;
                                Battleloop(enemy);
                                break;
                            }
                        }
                        if (!foundItem)
                        {
                            PrintError(decision);
                        }
                    }
                    else
                    {
                        if (decision == "9")
                        {
                            Battleloop(enemy);
                        }
                        else
                        {
                            PrintError(decision);
                        }
                    }
                }
                else
                {
                    SayLine("You dont have any potions to Use", ConsoleColor.Red);
                    Pause();
                    Battleloop(enemy);
                }
                EnemyAttack(enemy);
                break;
            case "3":
                Random rnd = new Random();
                int odds = rnd.Next(1, 20);

                if (odds == 1)
                {
                    SayLine("You escaped", ConsoleColor.DarkYellow);
                    Pause();
                    MainLoop();
                }
                else
                {
                    //Escape failed
                    int escapeDamage = CalculateEnemyDamage(enemy);
                    Clear();
                    SayLine($"You tried to escape but the {enemy.Name} caught you and dealt {escapeDamage} damage", ConsoleColor.Red);
                    player.CurrentHealth -= escapeDamage;
                    Pause();
                    Battleloop(enemy);
                }
                break;
            default:
                PrintError(decision);
                Break();
                Battleloop(enemy);
                break;
        }
    }
}

void EnemyAttack(Enemy enemy)
{
    Clear();

    if (enemy.CurrentHealth <= 0)
    {
        SayLine($"You have won the battle and earned {enemy.RewardAmount} gold", ConsoleColor.DarkYellow);
        player.GoldAmount += enemy.RewardAmount;
        Pause();
        MainLoop();
    }
    else if (player.CurrentHealth <= 0)
    {
        SayLine($"You have been defeated, you lost {enemy.RewardAmount} gold", ConsoleColor.DarkYellow);
        player.GoldAmount -= enemy.RewardAmount;
        Pause();
        MainLoop();
    }
    else
    {
        int enemyDamage = CalculateEnemyDamage(enemy);
        Clear();
        SayLine($"The {enemy.Name} attacked you and dealt {enemyDamage} damage", ConsoleColor.Red);
        player.CurrentHealth -= enemyDamage;
        Pause();
        Battleloop(enemy);
    }
}

int CalculateEnemyDamage(Enemy enemy)
{
    Random rnd = new Random();
    int odds = rnd.Next(1, 3);

    switch (odds)
    {
        case 1:
            return enemy.WeakDamage;
        case 2:
            return enemy.RegularDamage;
        default:
            return enemy.StrongDamage;
    }
}

//Function to console log and set console color on a new line
void SayLine(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}

//Function to console log and set console color
void Say(string message, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.Write(message);
    Console.ForegroundColor = ConsoleColor.White;
}

//Function to take input from console
string Listen()
{
    string message = Console.ReadLine();
    return message;
}

//Function to add linebreak to console
void Break()
{
    Console.WriteLine(" ");
}

//Function to clear console
void Clear()
{
    Console.Clear();
}

//Function to pause an wait for enter
void Pause()
{
    SayLine("Press 'Enter' to continue.", ConsoleColor.Gray);
    ConsoleKey key = ConsoleKey.Enter;

    while (Console.ReadKey(true).Key != key)
    { }
}

//Print error if input was not an option
void PrintError(string selection)
{
    SayLine($"{selection} is not an option", ConsoleColor.Red);
    SayLine("Press 'Enter' to continue.", ConsoleColor.Red);

    ConsoleKey key = ConsoleKey.Enter;
    while (Console.ReadKey(true).Key != key)
    { }
}

public class Player
{
    public int MaxHealt { get; set; }
    public int CurrentHealth { get; set; }
    public int GoldAmount { get; set; }

    public Weapon ActiveWeapon { get; set; }
    public Armor ActiveArmor { get; set; }

    public List<Weapon> weaponList = new List<Weapon>();
    public List<Armor> armorList = new List<Armor>();
    public List<Potion> potionList = new List<Potion>();

    //Player constructor
    public Player(int MAX_HEALTH, int GOLD_AMOUNT, Weapon START_WEAPON, Armor START_ARMOR)
    {
        MaxHealt = MAX_HEALTH;
        CurrentHealth = MAX_HEALTH;
        GoldAmount = GOLD_AMOUNT;

        ActiveWeapon = START_WEAPON;
        weaponList.Add(START_WEAPON);
        START_WEAPON.Owned = true;
        START_WEAPON.isEquipped = true;

        ActiveArmor = START_ARMOR;
        armorList.Add(START_ARMOR);
        START_ARMOR.Owned = true;
        START_ARMOR.isEquipped = true;
    }

    //Returns a list of owned weapons
    public string ShowWeapons()
    {
        string weaponString = "";

        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponString += weaponList[i].Name + ", ";
        }

        return weaponString;
    }

    //Returns a list of owned armor
    public string ShowArmor()
    {
        string armorString = "";

        for (int i = 0; i < armorList.Count; i++)
        {
            armorString += armorList[i].Name + ", ";
        }

        return armorString;
    }

    //Returns a list of owned potions
    public string ShowPotions()
    {
        string potionString = "";

        for (int i = 0; i < potionList.Count; i++)
        {
            potionString += potionList[i].Name + ", ";
        }

        return potionString;
    }

    //Adds gold to player
    public void AddGold(int GOLD_AMOUNT)
    {
        GoldAmount += GOLD_AMOUNT;
    }

    //Removes gold from player
    public void RemoveGold(int GOLD_AMOUNT)
    {
        GoldAmount -= GOLD_AMOUNT;
        if (GoldAmount < 0)
        {
            GoldAmount = 0;
        }
    }

    //Handles damage made to player
    public void TakeDamage(int DAMAGE_AMOUNT)
    {
        CurrentHealth -= DAMAGE_AMOUNT;
    }

    public void SetActiveWeapon(Weapon weapon)
    {
        ActiveWeapon = weapon;
        weapon.isEquipped = true;
    }

    public void SetActiveArmor(Armor armor)
    {
        ActiveArmor = armor;
        armor.isEquipped = true;
    }
}

public class Weapon
{
    public string Name { get; set; }
    public int Cost { get; set; }
    public bool Owned { get; set; }
    public int Damage { get; set; }
    public bool isEquipped { get; set; }

    //Weapon constructor
    public Weapon(string NAME, int COST, int DAMAGE)
    {
        Name = NAME;
        Cost = COST;
        Damage = DAMAGE;
        Owned = false;
        isEquipped = false;
    }

    //Handles buying a weapon
    public void Buy(Player player, Weapon weapon)
    {
        //Add weapon to players weaponList, reduce player gold and set Owned to true
        if (!Owned && player.GoldAmount >= weapon.Cost)
        {
            player.weaponList.Add(weapon);
            player.RemoveGold(weapon.Cost);
            weapon.Owned = true;
        }
        else if (player.GoldAmount < weapon.Cost)
        {
            Console.WriteLine("You dont have enough gold");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
        else
        {
            Console.WriteLine("You already have that weapon");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
    }

    public void Sell(Player player, Weapon weapon)
    {
        player.weaponList.Remove(weapon);
        int weaponGoldReturn = weapon.Cost;
        player.AddGold(weaponGoldReturn);
        weapon.Owned = false;
        weapon.isEquipped = false;

        Console.WriteLine($"You got {weaponGoldReturn} gold from {weapon.Name}");
        Console.WriteLine("Press 'Enter' to continue.");
        ConsoleKey key = ConsoleKey.Enter;

        while (Console.ReadKey(true).Key != key)
        { }
    }

    public void DisplayWeaponStats(bool indent)
    {
        if (indent)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"     Damage: {Damage}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Damage: {Damage}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    //Checks if weapon is owned
    public string IsOwned()
    {
        if (Owned)
        {
            return "Owned";
        }
        else
        {
            return null;
        }
    }
}

class WoodenSword : Weapon
{
    public WoodenSword() : base("Wooden Sword", 160, 5)
    {

    }
}

class SteelSword : Weapon
{
    public SteelSword() : base("Steel Sword", 460, 16)
    {

    }
}

class FireSword : Weapon
{
    public FireSword() : base("Fire Sword", 760, 28)
    {

    }
}

public class Armor
{
    public string Name { get; set; }
    public int Cost { get; set; }
    public bool Owned { get; set; }
    public int Protection { get; set; }
    public bool isEquipped { get; set; }

    //Armor constructor
    public Armor(string NAME, int COST, int PROTECTION)
    {
        Name = NAME;
        Cost = COST;
        Protection = PROTECTION;
        Owned = false;
        isEquipped = false;
    }

    //Handles buying armor
    public void Buy(Player player, Armor armor)
    {
        //Add armor to players armorList, reduce player gold and set Owned to true
        if (!Owned && player.GoldAmount >= armor.Cost)
        {
            player.armorList.Add(armor);
            player.RemoveGold(armor.Cost);
            armor.Owned = true;
        }
        else if (player.GoldAmount < armor.Cost)
        {
            Console.WriteLine("You dont have enough gold");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
        else
        {
            Console.WriteLine("You already have that armor");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
    }

    public void Sell(Player player, Armor armor)
    {
        player.armorList.Remove(armor);
        int armorGoldReturn = armor.Cost;
        player.AddGold(armorGoldReturn);
        armor.Owned = false;
        armor.isEquipped = false;

        Console.WriteLine($"You got {armorGoldReturn} gold from {armor.Name}");
        Console.WriteLine("Press 'Enter' to continue.");
        ConsoleKey key = ConsoleKey.Enter;

        while (Console.ReadKey(true).Key != key)
        { }
    }

    public void DisplayArmorStats(bool indent)
    {
        if (indent)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"     Protection: {Protection}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Protection: {Protection}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    //Checks if armor is owned
    public string IsOwned()
    {
        if (Owned)
        {
            return "Owned";
        }
        else
        {
            return null;
        }
    }
}

public class LeatherArmor : Armor
{
    public LeatherArmor() : base("Leather Armor", 1060, 68)
    {

    }
}

public class BronzeArmor : Armor
{
    public BronzeArmor() : base("Bronze Armor", 3460, 86)
    {

    }
}

public class IronArmor : Armor
{
    public IronArmor() : base("Iron Armor", 5160, 158)
    {

    }
}

public class Potion
{
    public string Name { get; set; }
    public int Cost { get; set; }
    public bool Owned { get; set; }
    public int Healing { get; set; }

    //Potion constructor
    public Potion(string NAME, int COST, int HEALING)
    {
        Name = NAME;
        Cost = COST;
        Healing = HEALING;
        Owned = false;
    }

    //Handles buying potion
    public void Buy(Player player, Potion potion)
    {
        //Add potion to players potionList, reduce player gold and set Owned to true
        if (!Owned && player.GoldAmount >= potion.Cost)
        {
            player.potionList.Add(potion);
            player.RemoveGold(potion.Cost);
            potion.Owned = true;
        }
        else if (player.GoldAmount < potion.Cost)
        {
            Console.WriteLine("You dont have enough gold");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
        else
        {
            Console.WriteLine("You already have that potion");
            Console.WriteLine("Press 'Enter' to continue.");
            ConsoleKey key = ConsoleKey.Enter;

            while (Console.ReadKey(true).Key != key)
            { }
        }
    }

    public void Sell(Player player, Potion potion)
    {
        player.potionList.Remove(potion);
        int potionGoldReturn = potion.Cost;
        player.AddGold(potionGoldReturn);
        potion.Owned = false;

        Console.WriteLine($"You got {potionGoldReturn} gold from {potion.Name}");
        Console.WriteLine("Press 'Enter' to continue.");
        ConsoleKey key = ConsoleKey.Enter;

        while (Console.ReadKey(true).Key != key)
        { }
    }

    //Checks if potion is owned
    public string IsOwned()
    {
        if (Owned)
        {
            return "Owned";
        }
        else
        {
            return null;
        }
    }

    public void Consume(Player player, Potion potion)
    {
        if (potion.Healing + player.CurrentHealth > player.MaxHealt)
        {
            player.CurrentHealth = player.MaxHealt;
        }
        else
        {
            player.potionList.Remove(potion);
            potion.Owned = false;
            player.CurrentHealth += potion.Healing;
        }
    }
}

public class HealingPotionWeak : Potion
{
    public HealingPotionWeak() : base("Weak Healing Potion", 200, 20)
    {

    }
}

public class HealingPotionMedium : Potion
{
    public HealingPotionMedium() : base("Medium Healing Potion", 400, 40)
    {

    }
}

public class HealingPotionStrong : Potion
{
    public HealingPotionStrong() : base("Strong Healing Potion", 600, 60)
    {

    }
}

public abstract class Enemy
{
    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int RewardAmount { get; set; }
    public int RegularDamage { get; set; }
    public int WeakDamage { get; set; }
    public int StrongDamage { get; set; }

    //Enemy constructor
    public Enemy(string NAME, int MAX_HEALTH, int REWARD_AMOUNT, int REGULAR_DAMAGE, int WEAK_DAMAGE, int STRONG_DAMAGE)
    {
        Name = NAME;
        MaxHealth = MAX_HEALTH;
        CurrentHealth = MAX_HEALTH;
        RewardAmount = REWARD_AMOUNT;
        RegularDamage = REGULAR_DAMAGE;
        WeakDamage = WEAK_DAMAGE;
        StrongDamage = STRONG_DAMAGE;
    }
}

public class Dragon : Enemy
{
    public Dragon() : base("Dragon", 100, 60, 24, 12, 36)
    {

    }
}

public class Golem : Enemy
{
    public Golem() : base("Golem", 200, 90, 20, 6, 28)
    {

    }
}

public class Goblin : Enemy
{
    public Goblin() : base("Goblin", 60, 30, 36, 24, 42)
    {

    }
}