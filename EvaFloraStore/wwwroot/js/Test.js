document.getElementById('submitBtn').addEventListener('click', checkRequiredFields);
function checkRequiredFields() {
    var requiredFields = document.querySelectorAll('input[required]');
    var isAllFieldsFilled = true;

    requiredFields.forEach(function (field) {
        if (field.value.trim() === '') {
            isAllFieldsFilled = false;
            return;
        }
    });

    if (isAllFieldsFilled) {
        $('#exampleModal').modal('show');
        return true;
    }
    else {
        return false;
    }
}