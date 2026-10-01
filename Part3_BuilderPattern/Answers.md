Task 3.1 — Question 1

### 1. Why is a single 20-parameter constructor a problem?

A single 20-parameter constructor creates several practical problems:

**1. Poor call-site readability:**
A 20-parameter constructor makes the code difficult to read because the caller must remember the position and meaning of many values. It is not immediately clear what each argument represents.

**2. Risk of passing values in the wrong order:**
A long constructor increases the risk of accidentally swapping arguments. This is especially dangerous when multiple parameters have the same type, such as decimal amounts or string address fields. The compiler may not detect the mistake because the values still have the correct types.

**3. Difficult to extend:**
If another optional property is added later, the constructor becomes even longer and more difficult to use. Existing constructor calls may also need to be updated when the constructor signature changes.

Task 3.1 — Question 2
### 2. Is this purely a constructor-length problem?

No. There is a deeper design issue. The problem is not only that the constructor has too many parameters, but also that a single class contains many loosely related properties that belong to different concepts.

In general, when a class contains several groups of closely related data, those groups can often be represented by smaller objects with their own responsibilities. This makes the design more organized and easier to understand, maintain, and extend.

For example, in an invoice, customer information, address information, and order/payment information represent different concepts. The billing and shipping fields can be grouped into an `Address` object, while the order and payment fields can be grouped into an `OrderInformation` object.

---------------------------------------------------------------------------------------------------------------------

Task 3.3 — Question 1

### 1.Why is this composed version better than the single big builder from Task 3.2? Consider: Single responsibility — what does each small builder own, and only own?

Answer:
The composed version is better because each builder has one clear responsibility. AddressBuilder is responsible only for creating and validating an Address. OrderBuilder is responsible only for creating and validating OrderInformation, including calculating the total amount. InvoiceBuilder is responsible for invoice-level information and composing the already-built address and order objects into the final Invoice. This keeps each builder focused and makes the code easier to understand, maintain, and modify.



Task 3.3 — Question 2

### 2.Independent validation — can AddressBuilder guarantee a complete address on its own, without the parent object knowing anything about street/city/zip rules?

Answer:
Yes. AddressBuilder can guarantee that an address is complete on its own because it owns all validation rules related to the address, such as checking that the street, city, state, ZIP code, and country are provided. Therefore, InvoiceBuilder does not need to know the details of the address validation rules. It only needs to receive a valid Address object.


Task 3.3 — Question 3

### 3.Reuse — the exact same AddressBuilder is used for both billing and shipping. What would you have had to duplicate without it?

Answer:
Using the same AddressBuilder for both billing and shipping avoids duplicating the address construction and validation logic. Without reuse, we would need separate builders such as BillingAddressBuilder and ShippingAddressBuilder, each containing the same properties, fluent methods, and validation rules. This would duplicate code and make future changes harder because the same logic might need to be updated in multiple places.


### 4.Readability at the call site — compare constructing the object with Task 3.2's single builder versus this composed version.

Answer:
The composed version improves readability at the call site because the construction is divided into meaningful steps. With the single builder from Task 3.2, all customer, billing, shipping, and order/payment properties are configured in one long chain, which can make the code harder to read.

With the composed version, each related group is built separately and given a clear name, such as `billingAddress`, `shippingAddress`, and `order`. These objects are then passed to `InvoiceBuilder`. This makes the structure of the final `Invoice` easier to understand and clearly shows how its parts are composed.

The comparison between the two approaches can be seen in `Program.cs` under the regions `Task 3.2 — Solve With a Builder (Test the Builder)` and `Task 3.3 — Refactor Into Smaller, Composed Builders`.
