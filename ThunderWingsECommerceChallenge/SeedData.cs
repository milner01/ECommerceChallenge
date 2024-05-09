using ThunderWingsECommerceChallenge.Models;

namespace ThunderWingsECommerceChallenge;

public static class SeedData
{
    public static void Initialize(ECommerceContext context)
    {
        // Seed your data here
        var aircrafts = new List<Aircraft>
        {
               new Aircraft(1, "F-22 Raptor", "Lockheed Martin", "United States of America", "Stealth air superiority fighter", 1498, 150000000),
               new Aircraft(2, "F-35 Lightning II", "Lockheed Martin", "United States of America", "Stealth Multirole fighter", 1200, 85000000),
               new Aircraft(3, "Sukhoi Su-35", "Sukhoi", "Russia", "Multirole fighter", 2400, 85000000),
               new Aircraft(4, "Su-57", "Sukhoi", "Russia", "Air superiority fighter", 1520, 70000000),
               new Aircraft(5, "Eurofighter Typhoon", "Airbus, BAE Systems, Leonardo, and others", "European consortium (Germany, Spain, Italy, and the United Kingdom)", "Multirole fighter", 1550, 100000000),
               new Aircraft(6, "F-15 Eagle", "Boeing", "United States of America", "Air superiority fighter", 1650, 30000000),
               new Aircraft(7, "Rafale", "Dassault Aviation", "France", "Multirole fighter", 1912, 80000000),
               new Aircraft(8, "Dassault Mirage 2000", "Dassault Aviation", "France", "Multirole fighter", 2336, 1000000),
               new Aircraft(9, "Chengdu J-10", "Chengdu Aircraft Industry Group", "China", "Multirole fighter", 2335, 60000000),
               new Aircraft(10, "J-20", "Chengdu Aerospace Corporation", "China", "Air superiority fighter", 1305, 110000000),
               new Aircraft(11, "Gripen E", "Saab", "Sweden", "Multirole fighter", 1372, 85000000),
               new Aircraft(12, "MiG-35", "Mikoyan", "Russia", "Multirole fighter", 1491, 40000000),
               new Aircraft(13, "F/A-18 Super Hornet", "Boeing", "United States of America", "Multirole fighter", 1190, 70000000),
               new Aircraft(14, "HAL Tejas", "Hindustan Aeronautics Limited (HAL)", "India", "Multirole fighter", 1370, 40000000),
               new Aircraft(15, "Mitsubishi F-2", "Mitsubishi Heavy Industries", "Japan", "Multirole fighter", 1860, 100000000),
               new Aircraft(16, "JF-17 Thunder", "Chengdu Aircraft Corporation (CAC) and Pakistan Aeronautical Complex (PAC)", "Pakistan", "Multirole fighter", 1975, 25000000),
               new Aircraft(17, "HAL AMCA", "Hindustan Aeronautics Limited (HAL)", "India", "Multirole fighter", 2485, 120000000),
               new Aircraft(18, "T-50 PAK FA", "Sukhoi", "Russia", "Air superiority fighter", 2495, 120000000),
               new Aircraft(19, "Chengdu J-7", "Chengdu Aircraft Corporation", "China", "Interceptor fighter", 2330, 20000000),
               new Aircraft(20, "Saab 37 Viggen", "Saab", "Sweden", "Ground-attack aircraft", 2350, 30000000),
               new Aircraft(21, "Mikoyan MiG-29", "Mikoyan", "Russia", "Multirole fighter", 2445, 45000000),
               new Aircraft(22, "Chengdu J-9", "Chengdu Aircraft Corporation", "China", "Stealth multirole fighter", 2600, 150000000),
               new Aircraft(23, "Sukhoi Su-30", "Sukhoi", "Russia", "Multirole fighter", 2120, 90000000),
               new Aircraft(24, "Northrop F-20 Tigershark", "Northrop Corporation", "United States of America", "Lightweight fighter", 2290, 35000000)            // Add more aircraft objects here...
        };

        context.Aircraft.AddRange(aircrafts);
        context.SaveChanges();
    }
}