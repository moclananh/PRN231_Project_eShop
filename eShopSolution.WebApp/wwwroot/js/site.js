var SiteController = function () {
    this.initialize = function () {
        regsiterEvents();
        loadCart();
        loadData();
        setHeightCard();
    }

    function setHeightCard() {
        var maxHeight = 0;
        for (i = 0; i < $(".thumbnails .span2 .thumbnail").length; i++) {
            if ($(".thumbnails .span2 .thumbnail").eq(i)) {
                var currentHeight = $(".thumbnails .span2 .thumbnail").eq(i).height();
                if (currentHeight >= maxHeight) {
                    maxHeight = currentHeight;
                }
            }
            else {
                break;
            }
        }
        $(".thumbnails .span2 .thumbnail").height(maxHeight);
    }
    function loadCart() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetListItems',
            success: function (res) {
                $('#lbl_number_items_header').text(res.length);
            }
        });
    }
    function regsiterEvents() {
        // Write your JavaScript code.
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const culture = $('#hidCulture').val();
            let id = $(this).data('id');

            checkProductInCart(id)
                .then(function (found) {
                    if (found) {
                        let products = JSON.parse(localStorage.getItem('products')) || {};
                        let idToRetrieve = id;
                        let quantity = products[idToRetrieve] + 1;
                        products[id] = quantity;
                        localStorage.setItem('products', JSON.stringify(products));
                        updateCart(id, quantity);
                    } else {

                        $.ajax({
                            type: "POST",
                            url: "/" + culture + '/Cart/AddToCart',
                            data: {
                                id: id,
                                languageId: culture
                            },
                            success: function (res) {
                                $('#lbl_number_items_header').text(res.length);
                            },
                            error: function (err) {
                                console.log(err);
                            }
                        });
                        let products = JSON.parse(localStorage.getItem('products')) || {};
                        // Ví dụ, mỗi sản phẩm có một ID
                        let quantity = 1;
                        products[id] = quantity;
                        localStorage.setItem('products', JSON.stringify(products));
                    }
                })
                .catch(function (err) {
                    console.log(err);
                });
        });
    }

    function checkProductInCart(dataId) {
        return new Promise(function (resolve, reject) {
            const culture = $('#hidCulture').val();
            $.ajax({
                type: "GET",
                url: "/" + culture + '/Cart/GetListItems',
                success: function (res) {
                    if (res.length === 0) {
                        $('#tbl_cart').hide();
                    }
                    let found = false;
                    $.each(res, function (i, item) {
                        if (dataId == item.productId) {
                            found = true;
                            return false;
                        }
                    });
                    resolve(found);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
    }



    function updateCart(id, quantity) {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "POST",
            url: "/" + culture + '/Cart/UpdateCart',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                $('#lbl_number_items_header').text(res.length);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function loadData() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetListItems',
            success: function (res) {
                if (res.length === 0) {
                    $('#tbl_cart').hide();
                }
                var html = '';
                var total = 0;

                $.each(res, function (i, item) {
                    var amount = item.price * item.quantity;
                    html += "<tr>"
                        + "<td> <img width=\"60\" src=\"" + $('#hidBaseAddress').val() + item.image + "\" alt=\"\" /></td>"
                        + "<td>" + item.description + "</td>"
                        + "<td><div class=\"input-append\"><input class=\"span1\" style=\"max-width: 34px\" placeholder=\"1\" id=\"txt_quantity_" + item.productId + "\" value=\"" + item.quantity + "\" size=\"16\" type=\"text\">"
                        + "<button class=\"btn btn-minus\" data-id=\"" + item.productId + "\" type =\"button\"> <i class=\"icon-minus\"></i></button>"
                        + "<button class=\"btn btn-plus\" type=\"button\" data-id=\"" + item.productId + "\"><i class=\"icon-plus\"></i></button>"
                        + "<button class=\"btn btn-danger btn-remove\" type=\"button\" data-id=\"" + item.productId + "\"><i class=\"icon-remove icon-white\"></i></button>"
                        + "</div>"
                        + "</td>"

                        + "<td>" + numberWithCommas(item.price) + "</td>"
                        + "<td>" + numberWithCommas(amount) + "</td>"
                        + "</tr>";
                    total += amount;
                });
                $('#cart_body').html(html);
                $('#lbl_number_of_items').text(res.length);
                $('#lbl_total').text(numberWithCommas(total));
            }
        });
    }
}


function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}