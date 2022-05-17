import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router,Routes, Route} from 'react-router-dom';
import HomePage from './Components/HomePage/HomePage';
import PostPage from './Components/PostPage/PostPage';
import NavBar from './Components/NavBar/NavBar';
import SignUp from './Components/SignUp/SignUp';
import SignIn from './Components/SignIn/SignIn';
import Verification from './Components/Verification/Verification';
import ProfilePage from './Components/ProfilePage/ProfilePage';
import AdminPage from './Components/AdminPage/AdminPage';
import SignInAdmin from './Components/SignInAdmin/SignInAdmin';
import SignUpAdmin from './Components/SignUpAdmin/SignUpAdmin';
import ListOfUsers from './Components/AdminControlUser/ListOfUsers';
import PinPosts from './Components/PinPosts/PinPosts';
import Error from './Components/Error';
import Contact from './Components/Contact/Contact';

function App() {

  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route exact  path='/' element={< HomePage />} />
          <Route exact  path='/signin' element={< SignIn />} />
          <Route exact  path='/signup' element={< SignUp />} />
          <Route exact  path='/signinadmin' element={< SignInAdmin />} />
          <Route exact  path='/signupadmin' element={< SignUpAdmin />} />
          <Route exact  path='/adminhome' element={< AdminPage />} />
          <Route exact  path='/pinposts' element={< PinPosts />} />
          <Route exact  path='/adminusers' element={< ListOfUsers />} />
          <Route exact  path='/profilepage' element={< ProfilePage />} />
          <Route exact  path='/profilepage/:username' element={< ProfilePage />} />
          <Route exact  path='/verify' element={< Verification />} />
          <Route exact  path='/postpage' element={< PostPage />} />
          <Route exact  path='/postpage/:id' element={< PostPage />} />
          <Route exact  path='/contact' element={< Contact />} />
          <Route exact  path='*' element={< Error />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
