using DeliveryRepo;
public class ProgramUI {
    DeliveryRepository _repo = new DeliveryRepository();
    public void Run() {
        Seed();
        Order();
    }

    private void Order() {
        bool keepRunning = true;

        while (keepRunning) {
            Console.Clear();
            
            System.Console.WriteLine("Please select from the following options:\n"
            + "1. Add a new delivery.\n"
            + "2. View ALL deliveries\n"
            + "3. View EnRoute or Completed deliveries.\n"
            + "4. Update delivery status.\n"
            + "5. Cancel a delivery.\n"
            + "6. Exit.");

            string? input = Console.ReadLine();
            
            switch (input) 
            {
                case "1":
                    AddNewDelivery();
                    break;
                case "2":
                    ViewAllDeliveries();
                    break;
                case "3":
                    ViewSpecDeliveries();
                    break;
                case "4":
                    UpdateDeliveryStatus();
                    break;
                case "5":
                    CancelDelivery();
                    break;
                case "6":
                    System.Console.WriteLine("Thank you for using Warner Transit Federal. Have a great day!");
                    keepRunning = false;
                    break;
                default:
                    System.Console.WriteLine("Error. Please try again.");
                    break;
            }

            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


            }


    private void AddNewDelivery()
    {
        Console.Clear();

        DeliveryOrder newOrder = new DeliveryOrder();

        newOrder.OrderNum = _repo.GetAllRoutes().Count + 1;

        System.Console.WriteLine("Please choose the status of the order:\n"
        + "1. Scheduled\n"
        + "2. EnRoute\n"
        + "3. Complete\n"
        + "4. Cancelled\n"
        + "5. Fell Off Truck\n");
        string? statusString = Console.ReadLine();
        int statusInt = int.Parse(statusString);
        newOrder.DeliveryStatus = (DeliveryStatus)statusInt;

        System.Console.WriteLine("Please enter the delivery date(MM/DD/YYYY):\n");
        string? deliveryString = Console.ReadLine();
        newOrder.DeliveryDate = DateTime.Parse(deliveryString);

        System.Console.WriteLine("Please enter the customer ID number:\n");
        string? customerString = Console.ReadLine();
        newOrder.CustomerID = int.Parse(customerString);

        System.Console.WriteLine("Please enter a name for the order being delivered:");
        newOrder.Name = Console.ReadLine();

        System.Console.WriteLine("Please enter how many items are in this order:\n");
        string? quantityString = Console.ReadLine();
        newOrder.Quantity = int.Parse(quantityString);

        System.Console.WriteLine("Please enter the date this order was placed(MM/DD/YYYY):\n");
        string? orderString = Console.ReadLine();
        newOrder.OrderDate = DateTime.Parse(orderString);

        bool OrderAdded = _repo.AddNewDelivery(newOrder);

        if (OrderAdded) {
            Console.Clear();
            System.Console.WriteLine("Delivery Order added successfully!\n");
        } else {
            Console.Clear();
            System.Console.WriteLine("Delivery Order NOT added, please try again!\n");
        }
    }

    private void ViewAllDeliveries()
    {
        foreach (DeliveryOrder order in _repo.GetAllRoutes())
        {
            DisplayOrder(order);
        }
    }

    private void ViewSpecDeliveries() {

        bool deliveryRunning = true;
        while (deliveryRunning)
        {
            Console.Clear();

            foreach (DeliveryOrder order in _repo.GetAllRoutes())
            {
                System.Console.WriteLine("Would you like to view Completed Deliveries or EnRoute Deliveries?\n"
                + "2. EnRoute Deliveries\n"
                + "3. Completed Deliveries\n"
                + "4. Main Menu");
                string? deliveryString = Console.ReadLine();

                switch (deliveryString)
                {
                    case "2":
                    case "3":
                    int completeInt = int.Parse(deliveryString);
                    order.DeliveryStatus = (DeliveryStatus)completeInt;
                    DisplayOrder(order);
                        break;
                    case "4":
                    System.Console.WriteLine("Beep bop boop");
                    deliveryRunning = false;
                        break;
                    default:
                    System.Console.WriteLine("Incorrect Response. Please try again.");
                        break;
                }
            }
        }
    }

    private void UpdateDeliveryStatus() {
        Console.Clear();
        
        System.Console.WriteLine("Please enter the order number of the delivery you would like to update.");
        int orderNum = int.Parse(Console.ReadLine());
        DeliveryOrder newOrder = new DeliveryOrder();


        System.Console.WriteLine("Please select the appropriate delivery status for this order:"
        + "1. Scheduled\n"
        + "2. EnRoute\n"
        + "3. Complete\n"
        + "4. Cancelled\n"
        + "5. Fell Off Truck\n");
        string? statusString = Console.ReadLine();
        int statusInt = int.Parse(statusString);
        newOrder.DeliveryStatus = (DeliveryStatus)statusInt;

        bool updateSuccess = _repo.UpdateDeliveryStatus(orderNum, newOrder);

        if (updateSuccess)
        {
            Console.Clear();
            System.Console.WriteLine("Delivery Status update was successful!");
        } else {
            Console.Clear();
            System.Console.WriteLine("Unable to update Delivery Status. Please try again!");
        }
    }

    private void CancelDelivery() {
        Console.Clear();
        System.Console.WriteLine("Please enter the order number of the delivery you would like to cancel:");
        int orderNum = int.Parse(Console.ReadLine());

        bool deleteSuccess = _repo.CancelDelivery(orderNum);

        if (deleteSuccess) 
        {
            Console.Clear();
            System.Console.WriteLine("Delivery successfully cancelled!");
        } else {
            Console.Clear();
            System.Console.WriteLine("Delivery has NOT been cancelled. Please try again!");
        }
    }

    private void DisplayOrder(DeliveryOrder order) {
        
        System.Console.WriteLine($"Order #{order.OrderNum}: {order.DeliveryStatus} on {order.DeliveryDate}.\n"
        + "-----------------\n"
        + $"Customer #{order.CustomerID} ordered on {order.OrderDate}.\n"
        + $"Order includes: {order.Name} ({order.Quantity}).");
        System.Console.WriteLine();
    }
    
    private void Seed() {
        DeliveryOrder coffeeTable = new DeliveryOrder(_repo.GetAllRoutes().Count + 1, DeliveryStatus.Complete, new DateTime(2022,01,15), 555, new DateTime(2022,01,01), "Coffee Table", 1);
        _repo.AddNewDelivery(coffeeTable);

        DeliveryOrder phoneCharger = new DeliveryOrder(_repo.GetAllRoutes().Count + 1, DeliveryStatus.EnRoute, new DateTime(2022,08,03), 203, new DateTime(2022,07,30), "Phone Charger", 10);
        _repo.AddNewDelivery(phoneCharger);

        DeliveryOrder sheinOrder = new DeliveryOrder(_repo.GetAllRoutes().Count + 1, DeliveryStatus.FellOffTruck, new DateTime(2050,12,31), 666, new DateTime(2022,04,7), "Shein Order", 5);
        _repo.AddNewDelivery(sheinOrder);
    }
}