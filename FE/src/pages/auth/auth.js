import React, { useState } from "react";

import logo from '../../assets/logo1.svg';
import "./auth.scss";
import SignInForm from "../../components/sign-in-form/sign-in-form";
import SignUpForm from "../../components/sign-up-form/sign-up-form";

function Auth() {
    const [showSignUp, setShowSignUp] = useState(false);

    return (
        <div className="auth-page" >
            <div className="bg-waves effect"></div>
            <div className="bg-waves effect"></div>
            <div className="bg-waves effect"></div>
            <div className='auth-container'>
                {showSignUp ?
                    <>
                        <h2>Sign Up</h2>
                        <SignUpForm
                            onSuccesfulRegister={() => setShowSignUp(false)}
                            onSwitchButton={() => setShowSignUp(false)}
                        />
                    </>
                    :
                    <>
                        <h2>Sign In</h2>
                        <SignInForm
                            onSwitchButton={() => setShowSignUp(true)}
                        />
                    </>
                }
            </div>
            <img className="logo" src={logo} />
        </div >
    );
}


export default Auth;
