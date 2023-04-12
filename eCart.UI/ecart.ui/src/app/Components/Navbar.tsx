import React from 'react';
import {Link} from 'react-router-dom'


const Navbar: React.FC = () => {
    return(
<nav className="navbar navbar-expand-lg navbar-dark bg-primary">
  <div className="container-fluid">
    <a className="navbar-brand" href="#">Brand</a>
    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#main_nav" aria-expanded="false" aria-label="Toggle navigation">
      <span className="navbar-toggler-icon" />
    </button>
    <div className="collapse navbar-collapse" id="main_nav">
      <ul className="navbar-nav">
        <li className="nav-item active"> <a className="nav-link" href="#">Home </a> </li>
        <li className="nav-item"><a className="nav-link" href="#"> About </a></li>
        <li className="nav-item dropdown" id="myDropdown">
          <a className="nav-link dropdown-toggle" href="#" data-bs-toggle="dropdown">  Treeview menu</a>
          <ul className="dropdown-menu">
            <li> <a className="dropdown-item" href="#"> Dropdown item 1 </a></li>
            <li> <a className="dropdown-item" href="#"> Dropdown item 2 » </a>
              <ul className="submenu dropdown-menu">
                <li><a className="dropdown-item" href="#">Submenu item 1</a></li>
                <li><a className="dropdown-item" href="#">Submenu item 2</a></li>
                <li><a className="dropdown-item" href="#">Submenu item 3 » </a>
                  <ul className="submenu dropdown-menu">
                    <li><a className="dropdown-item" href="#">Multi level 1</a></li>
                    <li><a className="dropdown-item" href="#">Multi level 2</a></li>
                  </ul>
                </li>
                <li><a className="dropdown-item" href="#">Submenu item 4</a></li>
                <li><a className="dropdown-item" href="#">Submenu item 5</a></li>
              </ul>
            </li>
            <li><a className="dropdown-item" href="#"> Dropdown item 3 </a></li>
            <li><a className="dropdown-item" href="#"> Dropdown item 4 </a></li>
          </ul>
        </li>
      </ul>
    </div>
    {/* navbar-collapse.// */}
  </div>
  {/* container-fluid.// */}
</nav>


    );
}

export default Navbar;