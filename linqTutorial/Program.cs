using SalesLibrary;

/*
int[] nbrs = {
   754, 233, 509, 792, 700, 596, 833, 658, 998, 742,
   187, 754, 308, 914, 489, 867, 717, 586, 929, 467,
   460, 241, 770, 324, 599, 259, 120, 800, 336, 609,
   690, 134, 598, 249, 282, 574, 334, 956, 659, 214,
   435, 643, 809, 874, 906, 620, 328, 369, 426, 561
};
*/

//query syntax (nbrs divisible by 3)
/*
var divBy3 = from n in nbrs
             where n % 3 == 0 || n % 5 == 0
             orderby n //descending
             select n;
foreach(var nbr in divBy3)
{
    Console.Write($"{nbr} ");
}
*/

//method syntax (nbrs divisible by 3 or 5 and sorted)
/*
var divBy3or5 = nbrs.Where(n => n % 3 == 0 || n % 5 == 0).OrderBy(n => n).ToList();
foreach(var nbr in divBy3or5)
{
    Console.Write($"{nbr} ");
}
*/




var custCtrl = new CustomersController("localhost", "sqlexpress");

var customers = custCtrl.GetAll();

var customersByName = customers.Where(n => (n.City == "Cincinnati" && n.Sales < 50000)
                                        || (n.City == "Columbus" && n.Sales < 40000)
                                        || (n.City == "Cleveland" && (n.Sales < 20000 || n.Sales > 50000)))
                               .OrderBy(n => n.City)
                               .ThenBy(n => n.Sales);


var customersByCityandSales = from n in customers
                              where (n.City == "Cincinnati" && n.Sales < 50000)
                                 || (n.City == "Columbus" && n.Sales < 40000)
                                 || (n.City == "Cleveland" && (n.Sales < 20000 || n.Sales > 50000))
                              orderby n.City, n.Sales
                              select n;

foreach (var cust in customersByCityandSales)
{
    Console.WriteLine($"{cust.Id,2} | {cust.Name,-24} | {cust.City,-11} ,{cust.State,-2} | {cust.Sales,10:C} | {cust.Active,-5}");
}

custCtrl.CloseConnection(); 