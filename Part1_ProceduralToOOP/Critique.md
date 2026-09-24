# الأخطاء التصميمية الي لقيتها في الكود

## 1. استخدام Arrays بحجم ثابت

البرنامج محدد عدد ثابت للعملاء والمنتجات والطلبات وOrder Lines.
وده بيمنع إضافة بيانات أكتر من  ال limit.

## 2. استخدام Global Variables

معظم data البرنامج موجودة في Global Variables.
أي Function تقدر توصل للبيانات وتعدلها، وده بيترتب عليه أن مفيش مسؤوولية محددة عن الداتا وكمان لو حبينا نعدل الكود بعد كده هيبقى بصعوبة

## 3. استخدام Parallel Arrays

بيانات الـCustomer والـProduct والـOrder متوزعة على Arrays مختلفة.
ومفيش حاجة تربط بينهم غير index ولو حصل error في ال index الداتا مش هتبقى متسقة

## 4. استخدام Indexes بدل Objects

الـOrders والـOrder Lines بتستخدم Indexes للوصول للـCustomers والـProducts.
استخدام object هيخلي ال العلاقات والمسؤولية أوضح

## 5. ضعف الـValidation

بعض البيانات، زي اسم الـCustomer والـEmail، مش بيتم التأكد من صحتها
قبل مايتعملها assign

## 6. فصل الـData عن الـBehavior

البيانات موجودة في Global Arrays وال business logic موجود في Functions منفصلة
الأفضل إن البيانات والـBehavior المرتبط بيها يكونوا داخل نفس الـClass

## 7. خلط الـValidation والـLogic والـOutput

بعض الـFunctions بتعمل Validation وBusiness Logic وتعديل للبيانات
و print لل error في نفس الوقت.
وده بيخلي الـFunctions مسؤولة عن حاجات كتير.

## 8. خصم الـStock قبل الدفع

الـStock بيتخصم بمجرد إضافة المنتج للـOrder، حتى لو الـOrder
ما اتدفعش.
وده ممكن يسبب مشكلة في إدارة الـStock.

## 9. عدم وجود Order و OrderLine كـClasses

مفيش `Order` أو `OrderLine` كـObjects حقيقية.
بياناتهم متوزعة على Arrays مختلفة، وده بيخلي ال design مش مفهوم

## 10. الـBusiness Rules متوزعة

ال  logic الخاص بوظيفة واحدة متوزع على أكتر من function وده بيخلي احتمالية حدوث errors أكبر وكمان الكود عيبقى أصعب في التعديل والفهم

## 11. الـVIP Discount معمول بشكل Hard-coded

نسبة الخصم `10%` مكتوبة مباشرة داخل حساب الـOrder.
وده هيخلينا نعدل الكود لو حبينا نغير نسبة الخصم

## 12. الـMenu بيعمل أكتر من حاجة

الـMenu مسؤول عن اختيار العملية وقراءة الـInput وتجهيز البيانات.
وكمان بيستخدم `if/else if` كتير بدل `switch`، وده بيخلي تنظيمه مش أحسن حاجة