import { Routes, Route } from 'react-router-dom';
import './css/index.css';
import { Role } from './types';
import React, {lazy} from 'react';
const CreateBambooTab = lazy(() => import('./components/pages/CreateBambooTab'));
const CreateRecipeTab = lazy(() => import('./components/pages/CreateRecipeTab'));
const GetBambooSessions = lazy(() => import('./components/bamboo/GetBamboos'));
const GetBambooSession = lazy(() => import('./components/bamboo/GetBamboo'));
const Index = lazy(() => import('./components'));
const Create = lazy(() => import ('./components/pages/Create'));
const Explore = lazy(() => import ('./components/pages/Explore'));
const Home = lazy(() => import ('./components/pages/Home'));
const Liked = lazy(() => import ('./components/pages/Liked'));
const Login = lazy(() => import ('./components/pages/Login'));
const Recipe = lazy(() => import ('./components/pages/Recipe'));
const SignUp = lazy(() => import ('./components/pages/SignUp'));
const UserTab = lazy(() => import ('./components/pages/UserTab'));
const ViewRecipes = lazy(() => import ('./components/pages/ViewRecipes'));
const ProtectedRoutes = lazy(() => import('./helper/protectedRoutes'));
const Start = lazy(() => import('./components/pages/Start'));
const EditUser = lazy(() => import ('./components/user/EditUser'));
const ViewBambooSessions = lazy(() => import ('./components/pages/ViewBambooSessions'));

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<Index />} />
      <Route path="/login" element={<Login />} />
      <Route path='/signup' element={<SignUp />} />
      <Route path="/start" element={<Start />} />

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER, Role.USER ]} redirectPath="/start" />}>
        <Route path="/recipes" element={<ViewRecipes />} />
        <Route path="/recipes/:id" element={<Recipe />} />
        <Route path="/explore" element={<Explore />} />
        <Route path="/user/:id/liked" element={<Liked />} />
        <Route path="/user/:id" element={<UserTab />} />
        <Route path="/user/:id/edit" element={<EditUser />} />
        <Route path="/app" element={<Home />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN, Role.VERIFIEDUSER]} redirectPath="/app" />}>
        <Route path="/create" element={<Create />} />
        <Route path="/createRecipe" element={<CreateRecipeTab />} />
        <Route path="/createBamboo" element={<CreateBambooTab />} />
        <Route path="/bambooSessions" element={<ViewBambooSessions />} />
      </Route>

      <Route path='/' element={<ProtectedRoutes isAllowed={[Role.ADMIN]} redirectPath="/app" />}>
        //admin only routes
      </Route>

    </Routes>
  );
};

export default App;
