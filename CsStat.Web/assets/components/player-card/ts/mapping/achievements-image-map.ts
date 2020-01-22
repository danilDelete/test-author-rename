import icon0 from '../../img/0.svg';
import icon1 from '../../img/1.svg';
import icon2 from '../../img/2.svg';
import icon3 from '../../img/3.svg';
import icon4 from '../../img/4.svg';
import icon5 from '../../img/5.svg';
import icon6 from '../../img/6.svg';
import icon7 from '../../img/7.svg';
import icon8 from '../../img/8.svg';
import icon9 from '../../img/9.svg';
import icon10 from '../../img/10.svg';
import icon11 from '../../img/11.svg';
import icon12 from '../../img/12.svg';
import icon13 from '../../img/13.svg';

const MapAchievementIdToImage = (id: number): string | undefined => {
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
        case 9:
            return icon9;
        case 10:
            return icon10;
        case 11:
            return icon11;
        case 12:
            return icon12;
        case 13:
            return icon13;

        default:
            return undefined;
    }
};

export default MapAchievementIdToImage;
