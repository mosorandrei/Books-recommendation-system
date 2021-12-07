import { useState, useEffect } from "react";

import { handleValidation } from "../services/input-validation";

const useForm = (
    initialFieldsState = {},
    fieldsRules = {},
    onSubmit) => {
    const [fields, setFields] = useState(initialFieldsState);
    const [errors, setErrors] = useState({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleInputChange = e => {
        setFields(prevFields => ({
            ...prevFields,
            [e.target.name]: e.target.value
        }));
    };

    const handleSubmit = e => {
        e.preventDefault();
        setErrors(handleValidation(fieldsRules, fields));
        setIsSubmitting(true);
    };

    useEffect(
        () => {
            if (Object.keys(errors).length === 0 && isSubmitting) {
                onSubmit?.(fields);
            }
            setIsSubmitting(false);
        }, [errors, fields, isSubmitting, onSubmit]
    );

    const handleCustomChange = (property, value) => {
        setErrors(prevErrors => {
            delete prevErrors[property];
            return prevErrors;
        });
        setFields(prevFields => ({
            ...prevFields,
            [property]: value
        }));
    };

    return { fields, fieldsRules, errors, setErrors, handleInputChange, handleCustomChange, handleSubmit };
};

export default useForm;

