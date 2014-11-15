$(document).ready(function () {

    // Register Call
    $('.register-call').on("click", function () {
        var form = $(this).parent("form");
        $('div#formS').text($("form").serialize());
        console.log("registration");
        $.ajax({
            type: "POST",
            url: form.attr('action'),
            //url: "/call",
            dataType: 'json',
            ContentType: 'application/json',
            //data: { Comments: $('textarea#Comments').val(), Caller_Name: $('input#Caller_Name').val(), Caller_LastName: $('input#Caller_LastName').val() }
            //data: { comments: comments }
            data: $("form").serialize()
            //data: stringfied
        })
            .success(function (data) {
                console.log(data);
                alert(data.Comments);
            })
            .error(function () {
                alert("Your bid has been rejected");
            });

        return false;
    });

    // Bring data for that number
    $('input#PhoneNumber').on('focusout', function () {
        var phoneNumber = $('input#PhoneNumber').val();
        console.log(phoneNumber);
        $.ajax({
            type: 'POST',
            url: 'Call/Complete',
            data: { phoneNumber: phoneNumber },
            dataType: "json",
            ContentType: 'application/json'
        })
		        .success(function (data) {
		            console.log(data);
		            if (data !== false) {
		                $('input#Name').val(data.Name);
		                $('input#LastName').val(data.LastName);
		                $('input#Address1').val(data.Address1);
		                $('input#Address2').val(data.Address2);
		                $('input#PostCode').val(data.PostCode);
		            }
		            else {
		                $('input#Name').val('');
		                $('input#LastName').val('');
		                $('input#Address1').val('');
		                $('input#Address2').val('');
		                $('input#PostCode').val('');
		            }
		        })
		        .fail(function () {
		            // just in case posting your form failed
		            alert("Posting failed.");

		        });
        // to prevent refreshing the whole page page
        return false;
    });

    // Bring historical data for that number
    $('input#PhoneNumber').on('focusout', function () {
        var phoneNumber = $('input#PhoneNumber').val();
        console.log(phoneNumber);
        $.ajax({
            type: 'POST',
            url: 'Call/Historical',
            data: { phoneNumber: phoneNumber },
            dataType: "json",
            ContentType: 'application/json'
        })
		        .success(function (data) {
		            console.log(data);
		            if (data !== false) {
		                console.log(data);
		            }
		            else {
		                alert("no");
		            }
		        })
		        .fail(function () {
		            // just in case posting your form failed
		            alert("Posting failed to bring historical data.");

		        });
        // to prevent refreshing the whole page page
        return false;
    });
});