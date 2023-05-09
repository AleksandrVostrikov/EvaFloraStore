window.onload = function () {
    const quantityInputs = Array.from(document.getElementsByName('quantity'));
    async function onChange(e) {
        const inputId = e.target.id;
        const formElement = document.getElementById(`form_${inputId}`);
        const formData = new FormData(formElement);
        const productId = formData.get('productId');
        const quantity = formData.get('quantity');
        const params = new URLSearchParams({ productId, quantity });
        const response = await fetch(new URL(formElement.action + '?' + params));
        const responseBody = await response.json();
        const subtotalElement = document.getElementById(`subTotal_${inputId}`);
        subtotalElement.textContent = responseBody.subtotal;
        const totalElement = document.getElementById('total');
        totalElement.textContent = responseBody.total;
    }
    for (const input of quantityInputs) {
        input.addEventListener('change', onChange);
    }
};