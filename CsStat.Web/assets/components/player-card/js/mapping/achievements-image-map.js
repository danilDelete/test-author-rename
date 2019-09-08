import icon0 from '../../img/0.svg';
import icon1 from '../../img/1.svg';
import icon2 from '../../img/2.svg';
import icon3 from '../../img/3.svg';
import icon4 from '../../img/4.svg';
import icon5 from '../../img/5.svg';
import icon6 from '../../img/6.svg';
import icon7 from '../../img/7.svg';
import icon8 from '../../img/8.svg';

const MapAchievementIdToImage = (id) => {
    switch (id) {
        case 0:
            return icon0;
        case 1:
            return icon1;
        case 2:
            return icon2;
        case 3:
            return icon3;
        case 4:
            return icon4;
        case 5:
            return icon5;
        case 6:
            return icon6;
        case 7:
            return icon7;
        case 8:
            return icon8;

        default:
            return '';
    }
};

export default MapAchievementIdToImage;
