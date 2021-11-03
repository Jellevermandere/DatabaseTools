<?php
    class UserData { 
        public $username = ''; 
        public $data = ''; 

        function set_userData($username, $data){
            $this->username = $username;
            $this->data = $data;
        }
    }

    class UserDataArray{
        public $userDataArray = array();

        function add_element($element){
            $this->userDataArray[] = $element;
        }
    }
?>