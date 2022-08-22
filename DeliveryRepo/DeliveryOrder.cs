namespace DeliveryRepo;
public class DeliveryOrder
{
    public DeliveryOrder() {}

    public DeliveryOrder(int orderNum, DeliveryStatus deliveryStatus, DateTime deliveryDate, int customerID, DateTime orderDate, string name, int quantity) {
        OrderNum = orderNum;
        DeliveryStatus = deliveryStatus;
        DeliveryDate = deliveryDate;
        CustomerID = customerID;
        Name = name;
        Quantity = quantity;
        OrderDate = orderDate;
    }

    public int OrderNum { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int CustomerID { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } 
}

public enum DeliveryStatus { Scheduled =1, EnRoute, Complete, Cancelled, FellOffTruck }