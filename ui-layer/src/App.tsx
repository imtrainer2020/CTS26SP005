import { Routes, Route } from 'react-router-dom';
import Login from './pages/LoginRegister';
import ForgetPassword from './pages/ForgetPassword';

function App() {
    return (
        /* This is where the actual switching happens. 
           React Router will look at the URL and pick the matching Route.
        */
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/forgot-password" element={<ForgetPassword />} />
        </Routes>
    );
}

export default App;