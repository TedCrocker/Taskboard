import $ from "jquery";
import * as ReactDOM from "react-dom";
import * as React from "react";
import Button from "./Button";


//This kicks everything off
$(() =>
{
	
	ReactDOM.render(<Button />, $('#app')[0]);
});