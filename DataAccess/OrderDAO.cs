using BusinessObject.Model;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }

            }
        }

        public IEnumerable<Order> List()
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (var productManagerContext = new ProductManagerContext())
                {
                    orders = productManagerContext.Orders.ToList().OrderByDescending(order => order.OrderId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return orders;
        }


        public Order FindOne(Expression<Func<Order, bool>> predicate)
        {
            Order order = null;
            try
            {
                using (var productManagerContext = new ProductManagerContext())
                {
                    order = productManagerContext.Orders.SingleOrDefault(predicate);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }


        public Expression<Func<Order, bool>> BuildPredicate(OrderFilter filter)
        {
            Expression<Func<Order, bool>> predicate = null;

            if (filter != null)
            {
                if (filter.StartDate != null && filter.EndDate != null)
                {
                    predicate = order => order.OrderDate >= filter.StartDate && order.OrderDate <= filter.EndDate;
                }
                else if (filter.StartDate != null)
                {
                    predicate = order => order.OrderDate >= filter.StartDate;
                }
                else if (filter.EndDate != null)
                {
                    predicate = order => order.OrderDate <= filter.EndDate;
                }
            }

            return predicate;
        }
        public IEnumerable<Order> FindAll(Expression<Func<Order, bool>> predicate)
        {
            List<Order> orders = new List<Order>();
            using (var productManagerContext = new ProductManagerContext())
            {
                orders = productManagerContext.Orders.Where(predicate).ToList();
            }
            return orders;
        }

        public void Add(Order order)
        {
            try
            {
                Order p = FindOne(item => item.OrderId.Equals(order.OrderId));
                if (p == null)
                {
                    using (var saleManagement = new ProductManagerContext())
                    {
                        saleManagement.Orders.Add(order);
                        saleManagement.SaveChanges();
                    }

                }
                else
                {
                    throw new Exception("The order is already exist");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Order order)
        {
            try
            {
                Order p = FindOne(item => item.OrderId.Equals(order.OrderId));
                if (p != null)
                {
                    using (var productManagerContext = new ProductManagerContext())
                    {
                        productManagerContext.Orders.Remove(order);
                        productManagerContext.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The order does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                Order p = FindOne(item => item.OrderId.Equals(order.OrderId));
                if (p != null)
                {
                    using (var saleManager = new ProductManagerContext())
                    {
                        saleManager.Entry<Order>(order).State = EntityState.Modified;
                        saleManager.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The order does not exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
