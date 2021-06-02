    function checkGoogleSignedIn() {
        if(sessionStorage.getItem('userLoggedIn') == null){
            $("#sign-in-social-media").show()
        }
    }

    function onSignIn(googleUser) {
        var profile = googleUser.getBasicProfile();
        console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
        console.log('Name: ' + profile.getName());
        console.log('Image URL: ' + profile.getImageUrl());
        console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

        var userLoggedIn = {};
        userLoggedIn.Id = profile.getId();
        userLoggedIn.Name = profile.getName();
        sessionStorage.setItem('userLoggedIn',JSON.stringify(userLoggedIn));

        $("#sign-in-social-media").hide()
        $("#sign-out-social-media").html('<ul>\
            <li>Iniciaste sesión como <b>'+profile.getName()+'</b></li>\
            <li>|</li>\
            <li><a href="#" onclick="signOut();">Sign out</a></li>\
        </ul>')
        $("#sign-out-social-media").show()
    }

    function signOut() {
        var auth2 = gapi.auth2.getAuthInstance();
        auth2.signOut().then(function () {
            sessionStorage.removeItem('userLoggedIn');
            console.log('User signed out.');
            $("#sign-in-social-media").show()
            $("#sign-out-social-media").hide()
        });
        auth2.disconnect();
    }

    function test(){
        $("#gsi").slideToggle();
    }

    checkGoogleSignedIn()