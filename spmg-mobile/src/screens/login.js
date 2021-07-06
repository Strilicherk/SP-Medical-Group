import jwtDecode from "jwt-decode";
import api from '../services/api'
import React, { Component } from 'react';
import { Image, ImageBackground, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state ={
            email: '',
            senha: ''
        };
    }
    
    realizarLogin = async () => {
        console.warn(this.state.email + ' ' + this.state.senha);

        const resposta = await api.post('/login', {
            email: this.state.email,
            senha: this.state.senha,
        });

        const token = resposta.data.token;

        await AsyncStorage.setItem('userToken', token)

        console.warn(token)

        const valorToken = await AsyncStorage.getItem("userToken");

        if (jwtDecode(valorToken).role === "2") {
            this.props.navigation.navigate("Medic")
        } else if (jwtDecode(valorToken).role === "3") {
            this.props.navigation.navigate("Patient")
        }
    }

    render(){
        return (
            <ImageBackground
               source={require('../../assets/img/background-img.svg')}
               style={StyleSheet.absoluteFillObject} 
            >
                <View style={styles.overlay}/>
                <View style={styles.main}>
                    <Image 
                        source={require('../../assets/img/logo.png')}
                        style={styles.imgLogin}
                    />

                    <TextInput 
                        style={styles.loginInput}
                        placeholder="Insira seu e-mail"
                        placeholderTextColor="black"
                        keyboardType='email-address'
                        onChangeText={email => this.setState({ email })}
                    />
                    <TextInput 
                        style={styles.loginInput}
                        placeholder="Insira sua senha"
                        placeholderTextColor="black"
                        secureTextEntry={true}
                        onChangeText={senha => this.setState({ senha })}
                    />

                    <TouchableOpacity style={styles.loginBtn} onPress={this.realizarLogin}>  
                        <Text style={styles.loginBtnText}>Login</Text>
                    </TouchableOpacity>

                    <a>Esqueceu sua senha?</a>
                </View>
            </ImageBackground>
        )
    }
}

const styles = StyleSheet.create({
    
    overlay: {
        ...StyleSheet.absoluteFillObject,
        backgroundColor: 'rgba(74, 146, 212, 0.15)'
    },

    main: {
        width: '100%',
        height: '100%',
        alignItems: 'center',
        justifyContent: 'center'
    },
    imgLogin: {
        tintColor: '#1e3fd4',
        width: 77,
        height: 90,
        margin: 110,
        marginTop: 0
    },
    
    loginInput: {
        width: 135,
        height: 20,
        fontFamily: 'Roboto',
        fontWeight: 400,

        
    },

    loginBtn: {
        width: 133.67,
        height: 35.17,
        backgroundColor: '#56F6C1',

        borderRadius: 30,
        borderWidth: 1,
        borderColor: '#000',

        alignItems: 'center',
        justifyContent: 'center'
    }


});