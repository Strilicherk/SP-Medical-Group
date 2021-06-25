import api from '../services/api'
import React, { Component } from 'react';
import { Image, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
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

        this.props.navigation.navigate('Main')
    }

    render(){
        return (
        <View style={styles.login_container}>
            <View style={styles.login_start}>
                <View style={styles.login_start_text}>
                    <Text style={styles.login_start_title}>{'Login'.toUpperCase()}</Text>
                </View>
            </View>

            <View style={styles.login_form}>
                <View style={styles.login_form_input_email}>
                    <Text style={styles.login_form_text}>{'e-mail'.toUpperCase()}</Text>

                    <TextInput 
                        style={styles.login_form_input}
                        placeholder="e-mail"
                        placeholderTextColor="black"
                        keyboardType='email-address'
                        onChangeText={email => this.setState({ email })}
                    />
                </View>
                <View style={styles.login_form_input_senha}>
                    <Text style={styles.login_form_text}>{'senha'.toUpperCase()}</Text>

                    <TextInput 
                        style={styles.login_form_input}
                        placeholder="senha"
                        placeholderTextColor= "black"
                        secureTextEntry={true}
                        onChangeText={senha => this.setState({ senha })}
                    />
                </View>

                <TouchableOpacity style={styles.login_form_btn} onPress={this.realizarLogin}>  
                    <Text style={styles.login_form_btn_text}>{'Entrar'.toUpperCase()}</Text>
                </TouchableOpacity>
            </View>
        </View>
        )
    }
}

const styles = StyleSheet.create({
    login_container: {
        backgroundColor: '#2A3633',
        flex: 1,
        alignItems: 'center'
    },

    login_start: {
        justifyContent: 'center',
        maxWidth: 308,
        height: 270,
        justifyContent: 'space-between',
        marginTop: 40,
        marginBottom: 10
    },

    login_start_logo: {
        width: 310,
        height: 205
    },

    login_start_text: {
        justifyContent: 'center',
        alignItems: 'center'
    },

    login_start_title: {
        fontSize: 30,
        borderBottomColor: '#16DAE7',
        borderBottomWidth: 5,
        color: '#16DAE7'
    },
    
    login_form: {
        marginTop: 5,
        justifyContent: 'space-between',
        alignItems: 'center',
        width: 300,
        height: 240
    },
    
    login_form_input_email: {
        width: 300,
        height: 60,
        marginBottom: 5
    },

    login_form_text: {
        fontSize: 17,
        color: 'white',
        fontWeight: 'bold',
        marginBottom: 5
    },

    login_form_input: {
        backgroundColor: 'white',
        padding: 15,
        borderRadius: 7
    },

    login_form_input_senha: {
        width: 300,
        height: 60
    },

    login_form_btn: {
        backgroundColor: '#8312AB',
        width: 300,
        justifyContent: 'center',
        alignItems: 'center',
        paddingHorizontal: 100,
        paddingVertical: 20,
        borderRadius: 10,
        marginTop: 20
    },

    login_form_btn_text: {
        fontSize: 18,
        fontWeight: 600,
        color: 'white'
    }


});