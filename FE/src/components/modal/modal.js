import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { createPortal } from 'react-dom';
import PropTypes from 'prop-types';

import './modal.scss';

// eslint-disable-next-line react/display-name
const Modal = forwardRef(({ title, children }, ref) => {
    const [display, setDisplay] = useState(false);

    useImperativeHandle(ref, () => ({
        openModal: () => openModal(),
        closeModal: () => closeModal()
    })
    );

    const openModal = () => {
        setDisplay(true);
    };

    const closeModal = () => {
        setDisplay(false);
    };

    if (display) {
        return createPortal((
            <div className='modal-wrapper'>
                <div className='modal-background' onClick={() => closeModal()} />
                <div className="modal-container">
                    {title && <h2 className='modal-title'>{title}</h2>}
                    {children}
                </div>
            </div>
        ), document.getElementById('root')
        );
    }

    return null;
});

Modal.propTypes = {
    children: PropTypes.node
};

export default Modal;