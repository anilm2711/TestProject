function InitiPayPal()
{
    paypal.Button.render({
        //Configure environment
        env: 'sandbox',
        client: {
            sandbox: 'AWQZaQqjpGawvRoCsnj4ZjOWPwl0rLjMk51eEqJgN_Z8LEf6B7tQP6UQ8XyFk5spAnjhFTOVjzr9H_ib'
        },

        //Customize button
        locale: 'en_US',
        style: {
            size: 'small',
            color: 'gold',
            shape: 'pill'
        },
        commit: true,

        //Set up a payment
        payment: function (data, actions) {
            return actions.payment.create({
                transactions: [{
                    amount: {
                        total: _total,
                        currency: 'USD'
                    }
                }]
            });
        },

        //Execute the payment
        onAuthorize: function (data, actions) {
            return actions.payment.execute().then(function () {
                var url = '@Url.Action("CompleteOrder", "Orders", new { })';
                window.location.href = url;
            });
        }

    }, '#paypal-btn');
}
