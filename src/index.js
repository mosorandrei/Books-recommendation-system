import React from 'react';
import ReactDOM from 'react-dom';

import './index.scss';
import App from './app';
import AuthProvider from './hooks/auth-context';

ReactDOM.render(
	<React.StrictMode>
		<AuthProvider>
			<App/>
		</AuthProvider>
	</React.StrictMode>,
	document.getElementById('root')
);
