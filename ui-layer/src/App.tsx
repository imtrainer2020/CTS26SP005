import { Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import ForgetPassword from './pages/ForgetPassword';

function App() {
    return (
        //<Login />
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/forgot-password" element={<ForgetPassword />} />
        </Routes>
    );
}

export default App;