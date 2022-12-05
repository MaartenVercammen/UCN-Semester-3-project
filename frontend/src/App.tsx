import { Routes, Route } from 'react-router-dom';
import './css/index.css';
import { Role } from './types';
import React, {lazy} from 'react';
const CreateBamboo = lazy(() => import('./components/bamboo/createBamboo'));
const GetBamboo = lazy(() => import('./components/bamboo/getBamboo'));
const GetBamboos = lazy(() => import('./components/bamboo/getBamboos'));
const Index = lazy(() => import('./components'));
const Create = lazy(() => import ('./components/pages/Create'));
const Explore = lazy(() => import ('./components/pages/Explore'));
const Home = lazy(() => import ('./components/pages/Home'));
const Liked = lazy(() => import ('./components/pages/Liked'));
const Login = lazy(() => import ('./components/pages/Login'));
const Recipe = lazy(() => import ('./components/pages/Recipe'));
const SignUp = lazy(() => import ('./components/pages/SignUp'));
const UserTab = lazy(() => import ('./components/pages/UserTab'));
const GetRecipes = lazy(() => import ('./components/recipe/GetRecipes'));
const ProtectedRoutes = lazy(() => import('./helper/protectedRoutes'));
const Start = lazy(() => import('./components/pages/Start'));
const EditUser = lazy(() => import ('./components/user/EditUser'));

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/login" element={<Login />} />
      <Route path='/signup' element={<SignUp />} />
      <Route path="/start" element={<Start />} />

      {/* move this to protected later */}
      <Route path="/createBambooSession" element={<CreateBamboo />} />
      <Route path="/getBambooSession" element={<GetBamboo />} />
      <Route path="/getBambooSessions" element={<GetBamboos />} />

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER, Role.USER ]} redirectPath="/login" />}>
        <Route path="/recipes" element={<GetRecipes />} />
        <Route path="/recipes/:id" element={<Recipe />} />
        <Route path="/explore" element={<Explore />} />
        <Route path="/user/:id/liked" element={<Liked />} />
        <Route path="/user/:id" element={<UserTab />} />
        <Route path="/user/:id/edit" element={<EditUser />} />
        <Route path="/app" element={<Home />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER]} redirectPath="/app" />}>
        <Route path="/createRecipe" element={<Create />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN]} redirectPath="/app" />}>
        //admin only routes
      </Route>

    </Routes>
  );
};

export default App;
