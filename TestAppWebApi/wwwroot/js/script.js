
$(document).ready(function () {

    // Загрузка список магазинов с назначенными консультантами
    getList();

    // Вызов модальной формы создания нового магазина 
    // Проверка формы на валидность
    $("#addShopForm").validate({
        rules: {
            ShopName: {
                required: true,
            },
            Address: {
                required: true,
            }
        },
        messages: {
            ShopName: {
                required: "Введите название магазина",
            },
            Address: {
                required: "Введите адрес магазина",
            }
        },

        //Создание элементов html для отборажения сообщений о невалидности заполненных полей формы
        errorElement: "em",
        errorPlacement: function (error, element) {

            // Добавление "help-block"
            error.addClass("help-block");

            // Добавление `has-feedback` класса к родительскому классу
            // для добавления иконок к элементу input
            element.parents(".col-sm-8").addClass("has-feedback");
            error.insertAfter(element);
            if (!element.next("span")[0]) {
                $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            }
        },
        success: function (label, element) {
            if (!$(element).next("span")[0]) {
                $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-8").addClass("has-error").removeClass("has-success");
            $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-8").addClass("has-success").removeClass("has-error");
            $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
        },

        // Если форма добавления магазина валидна, вызывается функция добавления магазина
        submitHandler: async function (form) {
            const response = await fetch("api/shops/", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    ShopName: $('#ShopName').val(),
                    Address: $('#Address').val()
                }),
            });
            if (response.ok === true) {
                let data = response.json();
                alert("Магазин добавлен!");
                $('#addShopModal').modal('hide');
                $('.tbody').append(rows(data));
   
            }
            else {
                alert("Ошибка добавления");
                const errorData = await response.json();
                console.log("errors", errorData);
                if (errorData) {

                    // ошибки вследствие валидации по атрибутам
                    if (errorData.errors) {
                        if (errorData.errors["ShopName"]) {
                            addError(errorData.errors["ShopName"]);
                        }
                        if (errorData.errors["Address"]) {
                            addError(errorData.errors["Address"]);
                        }
                    }

                    // кастомные ошибки, определенные в контроллере
                    // добавляет ошибки свойства Name
                    if (errorData["ShopName"]) {
                        addError(errorData["ShopName"]);
                    }

                    // добавляет ошибки свойства Age
                    if (errorData["Address"]) {
                        addError(errorData["Address"]);
                    }
                }     
            }
        }
    });

    $("#addConsultantModal form").validate({
        rules: {
            Name: "required",
            Surname: "required"
        },
        messages: {
            Name: "Введите имя консультанта",
            Surname: "Введите фамилию консультанта"
        },
        errorElement: "em",
        errorPlacement: function (error, element) {

            // Добавление `help-block` класса к элементу error
            error.addClass("help-block");

            // Добавление `has-feedback` класса к родительскому div.form-group
            // для создания иконок для элементов input
            element.parents(".col-sm-8").addClass("has-feedback");
            error.insertAfter(element);
            if (!element.next("span")[0]) {
                $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            }
        },
        success: function (label, element) {
            if (!$(element).next("span")[0]) {
                $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-8").addClass("has-error").removeClass("has-success");
            $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".col-sm-8").addClass("has-success").removeClass("has-error");
            $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
        },

        //Если форма добавления консультанта валидна, вызывается функция добавления консультанта
        submitHandler: async function (form) {
            const response = await fetch("api/consultants/addconsultant", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    Name: $('#Name').val(),
                    Surname: $('#Surname').val()
                })
            });
            if (response.ok === true) {
                $('#addConsultantModal').modal('hide');
                alert("Консультант добавлен");
            }
            else {
                alert("Ошибка добавления");
            }
        }
    });

    // Функция назначения кандидата в магазин
    $("#btnAppoint").click(async function appointConsultant() {

        const response = await fetch("api/consultants", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                ShopId: $("#shops option:selected").val(),
                ConsultantId: $('#consultants option:selected').val()
            })
        });
        if (response.ok === true) {
            alert("Консультант назначен");
            $('#appointConsultantModal').modal('hide');
            getList();
        }
        else {
            alert("Ошибка назначения");
        }
    });

    // При нажатии на кнопку заполняются комбобоксы "Магазин" и "Консультант"
    // в модальной форме "Назначение консультанта"  
    $("#btnList").click(async function getShopsConsultants() {
        // отправление запроса и получение ответа
        const response = await fetch("api/consultants", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        // если запрос прошел успешно
        if (response.ok === true) {

            // получение данных
            var listData = await response.json();
            console.log(listData);
            $.each(listData.shops, function (index, value) {

                // заполнение комбобокса "Магазин" названиями магазинов.
                $("#shops").append('<option value="' + index + '">' + value + '</option>');
            });

            $.each(listData.consultants, function (index, value) {

                // заполнение комбобокса "Консультант" фамилиями и именами консультантов.
                $("#consultants").append('<option value="' + index + '">' + value + '</option>');
            });
        }
    });
});

//Функция загрузки списка магазинов с назначенными консультантами
async function getList() {
    // отправляет запрос и получает ответ
    const response = await fetch("api/shops/", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    // если запрос прошел успешно
    if (response.ok === true) {
        // получение данных
        const list = await response.json();
        
        $('.bg-info').html("<tr><th class ='text-center'>#</th>"+
            "<th class = 'text-center'> Магазин </th >" +
            "<th class= 'text-center'> Адрес </th>" +
            "<th class='text-center'> Консультант </th>" +
            "<th class='text-center'>Дата назначения</th></tr ></thead >");
        $('.tbody').html("");
        let rows = document.querySelector("tbody");
        list.forEach(shop => {
        // добавляем полученные элементы в таблицу
        rows.append(row(shop));
        });
    }
}

// Функция обработки клиентоской валидации
function addError(errors) {
    errors.forEach(error => {
        const p = document.createElement("p");
        p.append(error);
        document.getElementById("errors").append(p);
    });
}

// Функция динамической генерации таблицы магазинов
function row(shop) {

    console.log(shop);
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", shop.id);

    const idTd = document.createElement("td");
    idTd.append(shop.id);
    tr.append(idTd);

    const nameTd = document.createElement("td");
    nameTd.append(shop.shopName);
    tr.append(nameTd);

    const addressTd = document.createElement("td");
    addressTd.append(shop.address);
    tr.append(addressTd);

    const fullNameTd = document.createElement("td");
    $.each(shop.fullNames, function (index, value) {
        if ((value === null) || (typeof (value) === "undefined")) {
            fullNameTd.append('');
        }
        else {
            fullNameDiv = document.createElement("div");
            fullNameDiv.append(value);
            fullNameTd.append(fullNameDiv);
        }
    });
    tr.append(fullNameTd);
      
    const dateHiringTd = document.createElement("td");
    $.each(shop.dates, function (index, value) {
        if ((value === null) || (typeof (value) === "undefined")){
            dateHiringTd.append('');
        }
        else {
            dateHiringDiv = document.createElement("div");
            var newDate = new Date(value);
            dateHiringDiv.append(newDate.getDate() + '/' + newDate.getMonth() + '/' + newDate.getFullYear());
            dateHiringTd.append(dateHiringDiv);
        }
    });
    tr.append(dateHiringTd);
    return tr;
}

//Функция очистки html элементов
function clearTextBox() {
    $('#ShopName-error').remove();
    $('#Address-error').remove();
    $('#Name-error').remove();
    $('#inputShopName').find('span').remove();
    $('#inputAddress').find('span').remove();
    $('#inputName').find('span').remove();
    $('#Surname-error').remove();
    $('#Name').val("");
    $('#Surname').val("");
    $('#btnAddShop').show();
    $('#inputSurname').find('span').remove();
    $('#btnAddConsultant').show();
    $('#btnAppoint').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Surname').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $('#ShopName').css('border-color', 'lightgrey');
    $('#shops').html("");
    $('#consultants').html("");
}