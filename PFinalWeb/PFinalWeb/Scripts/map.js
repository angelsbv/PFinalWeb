var mapScript = document.querySelector('#_2kdsaqjh1');

function getCorrectPath(path){
    var origin = window.location.origin
    return origin+path
}

function callScript(script) 
{
    $.ajax({
        url: script,
        dataType: "script",
        async: true,           
        success: function () {
            loadMap();
        },
        error: function () {
            throw new Error(`No se pudo cargar el script ${script}`);
        }
    });
}

function insertCSS(url)
{
    var linkCSS = document.createElement('link')
    linkCSS.rel = "stylesheet"
    linkCSS.href = url
    document.head.appendChild(linkCSS);
}

function loadThings()
{   

    div = document.createElement('div')
    div.id = 'mapp'
    document.querySelector('.body-content').insertBefore(div, mapScript)

    hrefs = ['/Resources/leaflet/dist/leaflet.css', '/Resources/leaflet/dist/leaflet-extra-markers.min.css']
    for(i in hrefs){insertCSS(getCorrectPath(hrefs[i]))}

    srcs = ['/Resources/leaflet/dist/leaflet.js', '/Resources/leaflet/dist/leaflet-extra-markers.min.js']
    for(i in srcs){
        callScript(getCorrectPath(srcs[i]))
    }
}

const loadMap = function()
{
    mapContainer = document.querySelector('#mapp');
    
    if(mapContainer != null)
    {
        mapContainer.innerHTML = "<div id='map' style='width: 100%; height: 100%;'></div>";
        mapContainer.setAttribute("style", `height:490px;width:100%;margin:0px auto;margin-top:30px;margin-bottom:30px;`)
        map = L.map('map').setView([18.4666666667, -69.9], 7.23);
        
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { //carga capa desde leaflet 
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetmap</a> contributors' //y respectiva payola...(es gratis)
        }).addTo(map);

        var point = [18.4666666667, -69.9]; //punto por defecto //Aqui deberia ir vector con posiciones de los casos
        var marker = L.marker(point).bindPopup(`Localización`); //creamos marcador y su popup
        marker.addTo(map); //

        // var redMarker = L.ExtraMarkers.icon({
        //     icon: 'fa-exclamation-triangle',
        //     markerColor: 'red',
        //     shape: 'circle',
        //     prefix: 'fas'
        //   }).bindPopup(`Localización`);
        // L.marker(point, {icon: redMarker}).addTo(map);
    }
}
loadThings();