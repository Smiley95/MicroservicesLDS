/*!

=========================================================
* Black Dashboard React v1.0.0
=========================================================

* Product Page: https://www.creative-tim.com/product/black-dashboard-react
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/black-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import Dashboard from "views/dashboard";
import  Login  from "views/login";

var routes = [
  {
    path: "/dashboard",
    name: "Dashboard",
    //icon: "tim-icons icon-chart-pie-36",
    component: Dashboard,
    layout: "/expert"
  },
  {
    path: "/login",
    name: "Login",
    //icon: "tim-icons icon-align-center",
    component: Login,
    layout: ""
  }
];
export default routes;