# Answers

## 1. Why is a single 20-parameter constructor a problem?

A 20-parameter constructor is difficult to read and use because it contains many values with similar types.

It is easy to pass values in the wrong order, especially when multiple parameters are strings or decimals.

If another property is added later, the constructor becomes longer and every call to it may need to be updated.

## 2. Is the problem only the constructor length?

No. The deeper problem is that the Invoice class contains many loosely related groups of data, such as customer information, billing address, shipping address, and order/payment information.

Putting all these responsibilities in one large constructor and class makes the design harder to understand, maintain, and extend.
## 3. Why is the composed version better?

### 1. Single Responsibility

Each builder has one clear responsibility.

- AddressBuilder is responsible for building and validating an address.
- OrderBuilder is responsible for building order and payment information.
- InvoiceBuilder combines these parts to build the final invoice.

### 2. Independent Validation

AddressBuilder can validate that an address is complete by itself.

The InvoiceBuilder does not need to know the validation details of street, city, state, zip code, and country.

### 3. Reuse

The same AddressBuilder can be reused to create both the billing address and the shipping address.

Without it, we would need to duplicate the same address-building logic.

### 4. Readability

The composed version makes the construction process easier to understand because each part of the invoice is built separately.

Instead of one large builder handling everything, the code is divided into clear and logical parts.