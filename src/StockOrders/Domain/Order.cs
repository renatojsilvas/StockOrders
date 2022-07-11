using System;

namespace StockOrders.Domain
{
    public class Order
    {
        public Order(CreateOrderModel createOrder)
        {
            Id = createOrder.Id == null ? Guid.NewGuid() : createOrder.Id.Value;
            OrderDate = createOrder.OrderDate;
            Advisor = createOrder.Advisor;
            Account = createOrder.Account;
            Asset = createOrder.Asset;
            OrderType = createOrder.OrderType;
            Quantity = createOrder.Quantity;
            Value = createOrder.Value;
            Priority = createOrder.Priority;            
        }

        /// <summary>
        /// Id of order
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Order date
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Advisor name
        /// </summary>
        public string Advisor { get; private set; }

        /// <summary>
        /// Number of Account
        /// </summary>
        public int Account { get; private set; }

        /// <summary>
        /// Name of the asset
        /// </summary>
        public string Asset { get; private set; }

        /// <summary>
        /// Order type
        /// </summary>
        public OrderType OrderType { get; private set; }

        /// <summary>
        /// Number of the Assets unit
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Artificial value to keep the order hidden
        /// </summary>
        public int ApparentQuantity { get; private set; } = 0;

        /// <summary>
        /// Availbale quantity after some execution
        /// </summary>
        public int AvailableQuantity => Quantity - CancelledQuantity - ExecutedQuantity;

        /// <summary>
        /// Number of cancelled Assets units
        /// </summary>
        public int CancelledQuantity { get; private set; } = 0;

        /// <summary>
        /// Executed Assets units
        /// </summary>
        public int ExecutedQuantity { get; private set; } = 0;

        /// <summary>
        /// Price
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Price value where the order is placed
        /// </summary>
        public decimal? ValueTrigger { get; private set; } = null;

        /// <summary>
        /// Start price
        /// </summary>
        public decimal? Target { get; private set; } = null;

        /// <summary>
        /// Price where Start price will be placed
        /// </summary>
        public decimal? TargetTrigger { get; private set; } = null;

        /// <summary>
        /// Reduction of the start price
        /// </summary>
        public decimal? Reduction { get; private set; } = null;

        /// <summary>
        /// Priority
        /// </summary>
        public Priority Priority { get; private set; }

        /// <summary>
        /// Check if the order is executed
        /// </summary>
        public bool IsExecuted { get; private set; }

        /// <summary>
        /// Execute an operation by quantity
        /// </summary>        
        public void Execute(int quantity)
        {
            var executedQuantity = ExecutedQuantity;

            executedQuantity += quantity;
            if (executedQuantity >= Quantity)
            {
                executedQuantity = Quantity;
                IsExecuted = true;
            }            

            ExecutedQuantity = executedQuantity;
        }        
    }
}
