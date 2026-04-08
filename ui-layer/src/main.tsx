import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import App from './App'; // Import App instead of the individual pages

const rootElement = document.getElementById('root');

if (rootElement) {
    createRoot(rootElement).render(
        <StrictMode>
            {/* BrowserRouter must wrap the App so hooks like useNavigate work inside it */}
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </StrictMode>
    );
}