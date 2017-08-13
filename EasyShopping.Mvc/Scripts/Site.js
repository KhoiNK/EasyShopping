$(document).ready(function () {
    //Handles menu drop down
    $('.dropdown-menu').find('form').click(function (e) {
        e.stopPropagation();
    });
});

window.onclick = function (e) {
    if (!e.target.matches('#dropdown')) {
        var myDropdown = document.getElementById("mydropdown");
        if (myDropdown != null) {
            if (myDropdown.classList.contains('in')) {
                myDropdown.classList.remove('in');
                myDropdown.classList.add('collapse');
                //myDropdown.hidden = true;
            }
        }
    }
}

function payment(e) {
    var totalPrice = e * 0.00004;
    var element = document.getElementById("paypal-button");
    if (element != null) {
        element.parentNode.removeChild(element);
        var paypaldiv = document.createElement("div");
        paypaldiv.setAttribute("id", "paypal-button");
        document.getElementById("paypalhere").appendChild(paypaldiv);
    }
    paypal.Button.render({
        
        env: 'sandbox', // Or 'sandbox'

        client: {
            sandbox: 'ActZqngFZGp0PMch_I5wAiIaYyn4PT2OomDpW6fdQshvdQLeNMgKvh01BcCb28VSFBS-b8bguB0mBJyt',
            production: 'xxxxxxxxx'
        },

        commit: true, // Show a 'Pay Now' button

        payment: function (data, actions) {
            return actions.payment.create({
                payment: {
                    transactions: [
                        {
                            amount: { total: totalPrice.toString(), currency: 'USD' }
                        }
                    ]
                }
            });
        },

        onAuthorize: function (data, actions) {
            return actions.payment.execute().then(function (payment) {
                document.getElementById("purchasebtn").click();
                // The payment is complete!
                // You can now show a confirmation message to the customer
            });
        }

    }, '#paypal-button');
}