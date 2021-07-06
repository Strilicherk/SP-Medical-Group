import React from "react";
import logo from "../Header/logo.svg";

export default function Header() {
    return(
        <header>
            <div className="header-content-adm">
                <div className="div-logo-adm">
                    <img src={logo} alt="logo do SP medical group" className="logo-adm" />
                </div>
                <div className="header-options-adm">
                    <a href="#" className="a-adm">Sair</a>
                </div>
            </div>
        </header>
    )
}