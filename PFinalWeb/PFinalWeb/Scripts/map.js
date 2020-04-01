let mapContainer = document.querySelector('#map');
mapContainer.setAttribute("style", `height: 490px;width: 100%;margin:0px auto;margin-top:10px;margin-bottom: 30px;`)
if(mapContainer != null)
{
    let map = L.map('map').setView([18.4666666667, -69.9], 7.23);
    
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetmap</a> contributors'
    }).addTo(map);

    var point = [18.4666666667, -69.9]; //punto por defecto //Aqui deberia ir vector con posiciones de los casos
    var marker = L.marker(point).bindPopup(`Localización`);
    marker.addTo(map);

    // var redMarker = L.ExtraMarkers.icon({
    //     icon: 'fa-exclamation-triangle',
    //     markerColor: 'red',
    //     shape: 'circle',
    //     prefix: 'fas'
    //   }).bindPopup(`Localización`);
    // L.marker(point, {icon: redMarker}).addTo(map);
}