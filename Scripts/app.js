var ViewModel = function(){
    var self = this;
    self.user = ko.observableArray();
    self.newuser = {
        Name: ko.observable().extend({
            required: true,
            maxLength: 30 
        }),
        Email: ko.observable().extend({
            required: true,
            email: true,
            maxLength: 50
        }),
        Gender: ko.observable().extend({
            required: true
        }),
        DRegister: ko.observable().extend({
            required: true
        }),
        Dates: ko.observableArray().extend({
            required: true
        }),
        ARequest: ko.observable().extend({
            required: true,
            maxLength: 100
        })
    }
    console.log(self.newuser);
    self.error = ko.observable();

    var usersUri = '/api/Users/';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null,
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }
    function getAllUsers() {
        ajaxHelper(usersUri, 'GET').done(function (data) {
            for (var i = 0; i < data.length; i++) {
                var temp = new Date(data[i].DRegister)
                var month = temp.getMonth();
                var date = temp.getDate();
                var year = temp.getFullYear();
                data[i].DRegister = month+'/'+ date+'/'+year;
            }
            self.user(data);
        });
    }

    self.addUser = function (formElement) {
        console.log("hererererer");
        var user = {
            Name: self.newuser.Name(),
            Email: self.newuser.Email(),
            Gender: self.newuser.Gender(),
            DRegister: new Date(self.newuser.DRegister()),
            Dates: self.newuser.Dates().toString(),
            ARequest: self.newuser.ARequest()
        };
        validator(user);
        ajaxHelper(usersUri, 'POST', user).done(function (u) {
            self.user.push(u);
        });
    }

    function validator(user) {
        var date1 = new Date(2019,1,1);
        var date2 = new Date(2019, 6, 30);
        if (user.Name == undefined) {
            document.getElementById('Name').innerHTML = "Name is required";
        } else if (user.Name.length > 30) {
            document.getElementById('Name').innerHTML = "Name is too long";
        }
        if (user.Email == undefined) {
            document.getElementById('Email').innerHTML = "Email is required";
        } else if (user.Email.length > 50) {
            document.getElementById('Email').innerHTML = "Email is too long";
        }
        if (user.Gender == undefined) {
            document.getElementById('Gender').innerHTML = "Gender is required";
        }
        if (user.ARequest == undefined) {
            document.getElementById('ARequest').innerHTML = "Field is required";
        } else if (user.ARequest.length > 100) {
            document.getElementById('Email').innerHTML = "Field is too long";
        }
        if (user.Dates == "") {
            document.getElementById('Dates').innerHTML = "Date is required";
        } else if (user.DRegister < date1) {
            document.getElementById('Dates').innerHTML = "Allowed date is between 1st jan 2019 to 30 june 2019";
        } else if (user.DRegister > date2) {
            document.getElementById('Dates').innerHTML = "Allowed date is between 1st jan 2019 to 30 june 2019";
        }
        if (isNaN(user.DRegister)) {
            document.getElementById('DRegister').innerHTML = "Date reg is required";
        }
    }

    getAllUsers();
};

ko.applyBindings(new ViewModel());
