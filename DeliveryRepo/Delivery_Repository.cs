namespace DeliveryRepo;

public class DeliveryRepository {
    protected readonly List<DeliveryOrder> _delivery = new List<DeliveryOrder>();

    // CRUD

    public bool AddNewDelivery(DeliveryOrder order) {
        int prevCount = _delivery.Count;

        _delivery.Add(order);

        return prevCount < _delivery.Count ? true : false;
    }

    public List<DeliveryOrder> GetAllRoutes() {
        return _delivery;
    }

    public bool UpdateDeliveryStatus(int orderNum, DeliveryOrder newOrder) {
        DeliveryOrder? oldOrder = _delivery.Find(order => order.OrderNum == orderNum);

        if (oldOrder != null) {
            oldOrder.DeliveryStatus = newOrder.DeliveryStatus != 0 ? newOrder.DeliveryStatus : oldOrder.DeliveryStatus;

            return true;
        } else {
            return false;
        }
    }

    public bool CancelDelivery(int OrderNum)  {
        DeliveryOrder? orderToDelete = _delivery.Find(order => order.OrderNum == OrderNum);

        bool deleteResult = _delivery.Remove(orderToDelete);

        return deleteResult;
    }
}