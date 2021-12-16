function isInputEmpty(input) {
    return !input;
}
function isArrayEmpty(input) {
    return input.length === 0;
}
function inputValidatePattern(input, pattern) {
    const regexPattern = new RegExp(pattern);
    return !regexPattern.test(input);
}
function inputLongerThan(input, maxLength) {
    return input.length >= maxLength;
}
function inputShorterThan(input, minLength) {
    return input.length <= minLength;
}
function inputEqualToField(input, fieldValue) {
    return input !== fieldValue;
}
export function handleValidation(validationRules, fields) {
    const errors = {};
    for (const [fieldName, fieldValue] of Object.entries(fields)) {
        const fieldRule = validationRules[fieldName];
        if (typeof fieldRule === 'undefined') {
            continue;
        }
        if (fieldRule.pattern &&
            inputValidatePattern(fieldValue, fieldRule.pattern.value)) {
            errors[fieldName] = fieldRule.pattern.message || `Field does not match the required pattern`;
        }
        if (fieldRule.isEqualTo &&
            inputEqualToField(fieldValue, fields[fieldRule.isEqualTo.field])) {
            errors[fieldName] = fieldRule.isEqualTo.message || `Field must be equal to ${fieldRule.isEqualTo.field}`;
        }
        if (fieldRule.maxLength &&
            inputLongerThan(fieldValue, fieldRule.maxLength.value)) {
            errors[fieldName] = fieldRule.maxLength.message || `Field can not be longer than ${fieldRule.maxLength.value}`;
        }
        if (fieldRule.minLength &&
            inputShorterThan(fields[fieldName], fieldRule.minLength.value)) {
            errors[fieldName] = fieldRule.minLength.message || `Field can not be shorter than ${fieldRule.minLength.value}`;
        }
        if ((fieldRule.required
            && Array.isArray(fieldValue) && isArrayEmpty(fieldValue))
            || (fieldRule.required && isInputEmpty(fieldValue))) {
            errors[fieldName] = fieldRule.required.message || `Field can not be empty`;
        }
    }
    return errors;
}