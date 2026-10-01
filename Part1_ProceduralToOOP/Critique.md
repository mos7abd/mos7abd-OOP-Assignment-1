# Critique of the Procedural Order System

## 1. Overview

The original application is a procedural C++ order management system that handles customers, products, orders, order lines, payments, stock, discounts, and console interaction.

Although the application works correctly for its current requirements, its design has several structural problems. The main issues are the extensive use of global variables, parallel arrays, fixed-size collections, array indexes to represent relationships, and functions that contain multiple responsibilities.

The following sections identify the main problems in the original implementation and explain how an Object-Oriented Programming design can improve it.

---

## 2. Excessive Global State

One of the biggest problems in the current implementation is the extensive use of global variables.

For example, customer data is stored in several global arrays:

```cpp
int customerCount = 0;
int customerIds[MAX_CUSTOMERS];
string customerNames[MAX_CUSTOMERS];
string customerEmails[MAX_CUSTOMERS];
string customerCities[MAX_CUSTOMERS];
bool customerIsVip[MAX_CUSTOMERS];
```

Products and orders are also represented using global arrays.

This means that many functions can directly access and modify the application's internal state.

For example, `addCustomer()` directly modifies the customer arrays and `customerCount`. Similarly, `calculateOrderTotal()` depends on several global arrays related to orders, products, and customers.

This creates strong coupling between the functions and the application's global state.

A better OOP design would group related data inside classes such as `Customer`, `Product`, and `Order`, reducing the amount of global state.

---

## 3. Parallel Arrays

The application represents one customer using several separate arrays:

```cpp
int customerIds[MAX_CUSTOMERS];
string customerNames[MAX_CUSTOMERS];
string customerEmails[MAX_CUSTOMERS];
string customerCities[MAX_CUSTOMERS];
bool customerIsVip[MAX_CUSTOMERS];
```

The same index must represent the same customer.

For example:

```cpp
customerIds[0]
customerNames[0]
customerEmails[0]
customerCities[0]
customerIsVip[0]
```

All five values represent one customer.

This creates an implicit relationship between the arrays. If one array becomes inconsistent with the others, customer data can become incorrect.

An OOP design can represent the customer as one object:

```cpp
class Customer
{
    int id;
    string name;
    string email;
    string city;
    bool isVip;
};
```

This makes the relationship between the customer's data explicit and easier to maintain.

---

## 4. Parallel Arrays for Order Lines

Order lines are also represented using parallel arrays:

```cpp
int lineProductIndexes[MAX_ORDERS][MAX_LINES_PER_ORDER];
int lineQuantities[MAX_ORDERS][MAX_LINES_PER_ORDER];
```

The product index and quantity must use the same position to represent one order line.

For example:

```cpp
lineProductIndexes[0][0]
lineQuantities[0][0]
```

represent the product and quantity of the first line of the first order.

The problem is that `OrderLine` is not represented as an actual entity.

An OOP design could introduce an `OrderLine` class:

```cpp
class OrderLine
{
    Product* product;
    int quantity;
};
```

This makes the relationship between the product and quantity explicit.

---

## 5. Fixed Maximum Limits

The application uses fixed limits:

```cpp
const int MAX_CUSTOMERS = 50;
const int MAX_PRODUCTS = 50;
const int MAX_ORDERS = 100;
const int MAX_LINES_PER_ORDER = 20;
```

The program therefore cannot naturally grow beyond these limits.

For example, after 50 customers are stored, `addCustomer()` rejects any additional customer.

This makes the application less flexible and requires manual boundary checks throughout the code.

A more flexible design could use standard containers such as:

```cpp
vector<Customer>
vector<Product>
vector<Order>
vector<OrderLine>
```

This would allow the collections to grow dynamically and reduce manual size management.

---

## 6. Repeated Search Logic

The application contains three similar search functions:

```cpp
findCustomerIndexById()
findProductIndexById()
findOrderIndexById()
```

All three functions use a similar loop to search for an ID and return an index.

For example:

```cpp
int findCustomerIndexById(int id)
{
    for (int i = 0; i < customerCount; i++)
    {
        if (customerIds[i] == id)
            return i;
    }

    return -1;
}
```

This creates duplicated search logic.

More importantly, these functions return indexes rather than meaningful objects. The caller must then use the index to access another global array.

An OOP design can encapsulate searching inside a repository or manager and return the actual object or a reference to it.

---

## 7. Relationships Represented by Array Indexes

The relationship between an order and its customer is represented by:

```cpp
int orderCustomerIndexes[MAX_ORDERS];
```

The relationship between an order line and its product is represented by:

```cpp
int lineProductIndexes[MAX_ORDERS][MAX_LINES_PER_ORDER];
```

This means that relationships between domain entities are hidden inside integer indexes.

For example:

```cpp
int customerIndex = orderCustomerIndexes[orderIndex];
```

The developer must know that this integer is an index into several customer arrays.

This increases complexity and makes the relationships harder to understand.

In an OOP design, an `Order` could directly reference a `Customer`, while an `OrderLine` could directly reference a `Product`.

---

## 8. Business Logic Mixed with User Interface

Several business functions directly use console output.

For example:

```cpp
cout << "ERROR: customer list is full.\n";
```

This pattern appears in functions such as:

* `addCustomer()`
* `createOrder()`
* `addLineToOrder()`
* `markOrderPaid()`

These functions are therefore tightly coupled to the console interface.

The business logic should ideally not depend on how the result is displayed.

Separating the user interface from the business logic would make the same functionality easier to reuse in a GUI application, web API, or automated test.

A better structure would be:

```text
Console UI
    |
    v
Business Logic
    |
    v
Domain Objects
```

---

## 9. `printOrder()` Has Too Many Responsibilities

The `printOrder()` function performs many different tasks.

It:

1. Searches for the order.
2. Retrieves the customer.
3. Displays order information.
4. Displays customer information.
5. Displays payment status.
6. Iterates through order lines.
7. Retrieves product information.
8. Calculates line totals.
9. Displays the final order total.

This means one function is responsible for searching, data retrieval, calculations, and presentation.

If the output format or calculation rules change, the same function may need to be modified.

The OOP design can separate these responsibilities. The `Order` class can manage order-related behavior, while the presentation layer can be responsible for displaying the information.

---

## 10. `calculateOrderTotal()` Depends on Many Global Arrays

The `calculateOrderTotal()` function depends on many global arrays:

```cpp
orderLineCounts
lineProductIndexes
lineQuantities
productPrices
orderCustomerIndexes
customerIsVip
```

The function therefore needs to understand the internal storage structure of customers, products, and orders.

It is not calculating the total from an `Order` object. Instead, it receives an index and uses that index to access data from several unrelated global arrays.

An OOP design could move this behavior into the `Order` class:

```cpp
double Order::calculateTotal()
{
    // calculate order total
}
```

The order would own its order lines and customer relationship, reducing its dependency on global arrays.

---

## 11. `addLineToOrder()` Has Multiple Responsibilities

The `addLineToOrder()` function performs many tasks:

* Finds the order.
* Checks whether the order is already paid.
* Checks the maximum number of lines.
* Finds the product.
* Validates the quantity.
* Checks product stock.
* Reduces product stock.
* Creates the order line.
* Updates the order line count.

This makes the function responsible for several business rules at the same time.

As the application grows, additional rules such as taxes, promotions, product limits, or special discounts could make this function increasingly complex.

An OOP design can distribute these responsibilities between classes such as `Order`, `Product`, `Customer`, and `OrderLine`.

---

## 12. Weak Encapsulation

The current implementation provides very little encapsulation.

For example, global data such as:

```cpp
productStock[]
orderIsPaid[]
customerIsVip[]
```

can be directly accessed and modified by functions throughout the program.

This means the program cannot strongly control how important business state is changed.

For example, an order can theoretically be marked as paid by directly changing:

```cpp
orderIsPaid[index] = true;
```

without using the business rules implemented in `markOrderPaid()`.

With OOP, fields can be private and state changes can be controlled through methods such as:

```cpp
class Order
{
private:
    bool isPaid;

public:
    void markAsPaid();
};
```

This provides stronger encapsulation and allows the class to enforce its own rules.

---

## 13. Difficult Testing

The current functions depend heavily on global state.

For example, testing `calculateOrderTotal()` requires correctly preparing customers, products, orders, order lines, counters, and indexes in several global arrays.

Testing `addLineToOrder()` also requires a valid global customer, product, order, and stock state.

This makes unit testing more difficult because individual functions are not isolated from the rest of the application.

An OOP design can create independent objects for testing:

```cpp
Customer customer(...);
Product product(...);
Order order(...);

order.addLine(product, 2);
```

The behavior of the `Order` object can then be tested more directly.

---

## 14. Difficult to Extend

The current design becomes harder to extend when new requirements are introduced.

For example, if the system needs to support:

* Payment methods
* Shipping
* Taxes
* Product categories
* Order cancellation
* Order status
* Multiple discount rules

the procedural implementation would likely require additional global arrays, counters, indexes, and conditional statements.

This would increase the complexity of the existing design.

In an OOP design, new concepts can be represented as classes or behaviors.

For example:

```text
Customer
Product
Order
OrderLine
Payment
Shipping
```

This provides a clearer structure for future development.

---

## 15. Tight Coupling Between Storage and Business Logic

The business logic is strongly connected to the way the data is stored.

For example, `calculateOrderTotal()` knows that order data is stored using:

```cpp
orderLineCounts
lineProductIndexes
lineQuantities
orderCustomerIndexes
```

and that product prices are stored in:

```cpp
productPrices
```

If the internal storage changes from fixed arrays to `vector`, a database, or another data structure, many business functions may need to be changed.

An OOP design can hide internal data representation behind classes and methods.

The rest of the application can work with objects instead of depending on the exact storage mechanism.

---

## 16. `main()` Depends on Many Global Functions

The `main()` function coordinates the application using several global functions:

```cpp
seedSampleData();
runDemoScenario();
printCustomers();
printProducts();
printAllOrders();
totalSalesPaidOnly();
runInteractiveMenu();
```

Although `main()` is not very large, the application's state and behavior are distributed across many global variables and functions.

There is no clear object that owns the complete order management system.

An OOP design could introduce an `OrderSystem` or `OrderService` class to coordinate the application's state and operations.

For example:

```cpp
int main()
{
    OrderSystem system;

    system.seedSampleData();
    system.runDemoScenario();
    system.runInteractiveMenu();
}
```

This gives the application a clearer structure and a clear owner for its state.

---

## 17. Overall Assessment

The original procedural implementation is functional and demonstrates the required business operations.

However, the main structural problems are:

1. Excessive global state.
2. Parallel arrays.
3. Fixed-size collections.
4. Relationships represented through array indexes.
5. Repeated search logic.
6. Mixed business logic and console presentation.
7. Weak encapsulation.
8. Functions with multiple responsibilities.
9. Strong coupling between business logic and data storage.
10. Difficult unit testing.
11. Difficult future extension.

These issues do not necessarily mean that the application is incorrect. The main concern is that the current structure becomes harder to maintain and extend as the application grows.

---

## 18. Proposed OOP Structure

A possible object-oriented design could contain the following classes:

```text
Customer
    - id
    - name
    - email
    - city
    - isVip

Product
    - id
    - name
    - price
    - stock

OrderLine
    - product
    - quantity

Order
    - id
    - customer
    - date
    - isPaid
    - orderLines

OrderSystem / OrderService
    - customers
    - products
    - orders
    - createOrder()
    - addCustomer()
    - addProduct()
    - findCustomer()
    - findProduct()
    - findOrder()
    - calculatePaidSales()
```

This structure represents the actual concepts of the system instead of representing them mainly as collections of unrelated primitive arrays.

The relationships also become explicit:

```text
Customer
    ^
    |
  Order
    |
    v
OrderLine
    |
    v
 Product
```

Each class can have a clear responsibility and can encapsulate its own data and behavior.

---

## 19. Conclusion

The original procedural Order System successfully implements customer management, product management, order creation, stock handling, payment status, VIP discounts, and sales calculation.

However, the implementation relies heavily on global variables, parallel arrays, array indexes, and functions that combine multiple responsibilities. Business logic is also directly coupled to console output and the underlying storage structure.

Refactoring the application using Object-Oriented Programming can address these problems by grouping related data and behavior into meaningful classes such as `Customer`, `Product`, `OrderLine`, and `Order`.

The goal of the refactoring is not simply to replace functions with classes. The main goal is to create clear responsibilities, encapsulate state, make relationships explicit, reduce coupling, improve testability, and make the system easier to maintain and extend.
