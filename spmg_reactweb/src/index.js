import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Route, BrowserRouter as Router, Switch, Redirect} from 'react-router-dom';
import { parseJwt, usuarioAutenticado } from './services/auth'

import './index.css';

import Login from './pages/login/Login';
import Medic from './pages/medic/medic';
import NotFound from './pages/notFound/notFound';
import Adm from './pages/adm/adm'
import Patient from './pages/patient/patient'

import reportWebVitals from './reportWebVitals';

const PermissaoAdm = ({ component : Component }) => (
  <Route 
    render = { props =>
      usuarioAutenticado() && parseJwt().role === "1" ?
      <Component {...props} /> :
      <Redirect to= '/' />
    }
  />
)

const PermissaoMedic = ({ component : Component }) => (
  <Route 
    render = { props =>
      usuarioAutenticado() && parseJwt().role === "2" ?
      <Component {...props} /> :
      <Redirect to= '/' />
    }
  />
)

const PermissaoPatient = ({ component : Component }) => (
  <Route 
    render = { props =>
      usuarioAutenticado() && parseJwt().role === "3" ?
      <Component {...props} /> :
      <Redirect to= '/' />
    }
  />
)

const routing = (
  <Router>
    <div>
      <Switch>
        <Route exact path="/" component={Login}/> 
        <PermissaoMedic path="/medic/consultas" component={Medic}/>
        <PermissaoAdm path="/adm/consultas" component={Adm}/>
        <PermissaoPatient path="/patient/consultas" component={Patient}/>
        <Route path="/notfound" component={NotFound}/>
      </Switch>
    </div>
  </Router>
)
ReactDOM.render(routing, document.getElementById('root'));

reportWebVitals();
