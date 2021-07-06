import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';

import Login from './src/screens/login'
import Medic from './src/screens/medic'
import Patient from './src/screens/patient'

const AuthStack = createStackNavigator();

export default function Stack() {
  return (
    <NavigationContainer>
      <AuthStack.Navigator headerMode='none'>
        <AuthStack.Screen name = 'Login' component={Login}/>
        <AuthStack.Screen name = 'Patient' component={Patient}/>
        <AuthStack.Screen name = 'Medic' component={Medic}/>
      </AuthStack.Navigator>
    </NavigationContainer>
  );
}


