$('#search').keyup(function () {
    //get data from json file
    var urlForJson = "/api/talents";

    //get data from Restful web Service in development environment
    //var urlForJson = "http://localhost:9000/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    var urlForCloudImage = "https://res.cloudinary.com/dgbrqt08w/image/upload/v1559278270/Map_DTTA_rmn5yt.jpg";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    $.ajax({
        dataType: 'json',
        url: "data.json",
    }).done(function (data) {
        console.log(data)
    })
    $.getJSON(urlForJson, function (data) {
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            if ((val.Name.search(myExp) != -1) ||
                (val.Bio.search(myExp) != -1)) {
                output += '<li>';
                output += '<h2>' + val.Name + '</h2>';
                //get the absolute path for local image
                //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                //get the image from cloud hosting
                // output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<img src=' + urlForCloudImage + " alt=image />";
                output += '<p>' + val.Bio + '</p>';
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});
