//function validatePhoneNumber() {
//    var phoneInput = document.getElementById("phoneNumberInput");
//    var phoneValue = phoneInput.value.trim();
//    var errorMessage = document.getElementById("phoneError");
//    var validPrefixes = ["077", "078", "079", "075", "076"];
//    var prefix = phoneValue.substring(0, 3);

//    // اقتطاع أي رقم زائد عن 11 أثناء الكتابة
//    if (phoneValue.length > 11) {
//        phoneInput.value = phoneValue.substring(0, 11);
//        phoneValue = phoneInput.value;
//    }

//    if (phoneValue.length !== 11) {
//        errorMessage.innerText = "يجب أن يكون رقم الهاتف 11 رقمًا.";
//        return false;
//    }

//    if (!validPrefixes.includes(prefix)) {
//        errorMessage.innerText = "يجب أن يبدأ رقم الهاتف بمفتاح شبكة صالح (مثل 077, 078, 079, 075, 076).";
//        return false;
//    }

//    if (!/^\d+$/.test(phoneValue)) {
//        errorMessage.innerText = "يجب أن يحتوي رقم الهاتف على أرقام فقط.";
//        return false;
//    }

//    errorMessage.innerText = ""; // إذا صحيح، تختفي الرسالة
//    return true;
//}

//// تحقق أثناء الكتابة
////document.getElementById("phoneNumberInput").addEventListener("input", validatePhoneNumber);

//// تحقق عند إرسال الفورم
//document.querySelector("form").addEventListener("submit", function (e) {
//    if (!validatePhoneNumber()) {
//        e.preventDefault(); // يمنع الإرسال
//        var phoneInput = document.getElementById("phoneNumberInput");
//        setTimeout(function () {
//            phoneInput.focus();
//        }, 10);
//    }
//});

//// تحقق عند تحميل الصفحة (حتى تظهر الرسالة من البداية إذا فارغ)
//window.addEventListener("load", function () {
//    validatePhoneNumber();
//});

///////////////// الملاحظات
//var noteInput = document.getElementById("noteInput");
//var noteError = document.getElementById("noteError");
//var noteCount = document.getElementById("noteCount");
//var maxLength = 100;

//// تحديث عداد الحروف
//function updateNoteCounter() {
//    var currentLength = noteInput.value.length;
//    noteCount.innerText = (maxLength - currentLength) + " حرف متبقي";
//}

//// التحقق أثناء الكتابة
//noteInput.addEventListener("input", function () {
//    if (noteInput.value.length >= maxLength) {
//        noteError.innerText = "وصلت الحد الأقصى 100 حرف.";
//    } else {
//        noteError.innerText = "";
//    }
//    updateNoteCounter();
//});

//// عند تحميل الصفحة
//window.addEventListener("load", function () {
//    updateNoteCounter();
//});

//// ===== تحقق عند إرسال الفورم =====
//document.querySelector("form").addEventListener("submit", function (e) {
//    var valid = validatePhoneNumber(); // افترض أن دالة validatePhoneNumber موجودة
//    if (noteInput.value.length > maxLength) {
//        noteError.innerText = "لا يمكن كتابة أكثر من 100 أحرف.";
//        noteInput.focus();
//        valid = false;
//    }
//    if (!valid) {
//        e.preventDefault();
//    }
//});



////البطاقة الموحده
//var personalCardInput = document.getElementById("personalCardInput");
//var personalCardError = document.getElementById("personalCardError");

//// التحقق أثناء الكتابة
//personalCardInput.addEventListener("input", function () {
//    // إزالة أي حروف غير أرقام
//    personalCardInput.value = personalCardInput.value.replace(/\D/g, '');

//    if (personalCardInput.value.length > 12) {
//        personalCardInput.value = personalCardInput.value.substring(0, 12);
//    }

//    if (personalCardInput.value.length < 12) {
//        personalCardError.innerText = "يجب أن يكون رقم البطاقة 12 رقمًا.";
//    } else {
//        personalCardError.innerText = "";
//    }
//});

//// التحقق عند إرسال الفورم
//document.querySelector("form").addEventListener("submit", function (e) {
//    if (personalCardInput.value.length !== 12) {
//        personalCardError.innerText = "يجب أن يكون رقم البطاقة 12 رقمًا.";
//        personalCardInput.focus();
//        e.preventDefault(); // يمنع الإرسال
//    }
//});
console.log("uuuuuuu")