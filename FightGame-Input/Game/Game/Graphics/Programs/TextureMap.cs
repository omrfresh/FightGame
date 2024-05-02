using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TextureMap
    {
        private TexturePoint[] TexturePoints { get; set; }
        /// <summary>
        /// Количество текстур в текстурной карте в ширину
        /// </summary>
        private double Width {  get; init; }
        /// <summary>
        /// Количество текстур в текстурной карте в высоту
        /// </summary>
        private double Heidth {  get; init; }
        /// <summary>
        /// Количество вершин в текстурке
        /// </summary>
        private int NumberVertex {  get; init; }
        /// <summary>
        /// Текстура
        /// </summary>
        public Texture Texture { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">Количество текстур в текстурной карте в ширину</param>
        /// <param name="hidth">Количество текстур в текстурной карте в высоту</param>
        /// <param name="numberVertices">Количество вершин в текстурке</param>
        /// <param name="texturePath">Путь к текстурной карте</param>
        public TextureMap(double width, double hidth, int numberVertices, Texture texture)
        {
            if (width == 0) width = 1;
            if (hidth == 0) hidth = 1;
            Width = width;
            Heidth = hidth;
            NumberVertex = numberVertices;
            TexturePoints = new TexturePoint[(int)(width * hidth * numberVertices)];
            Texture = texture;
            GenerateMap();
        }
        /// <summary>
        /// Генерация индексов текстур на основе текстурной карты, для
        /// будущего их использования, путем передачи координат текстур другим объектам
        /// </summary>
        private void GenerateMap()
        {
            int digits = 3;
            double UnitWidth = double.Round(1.0 / Width, digits);
            double UnitHeidth = double.Round(1.0 / Heidth, digits);
            // Верхний левый угол текстры соответствует координате (0,1)
            // Нижний правый угол текстуры соответствует координате (1,0)
            // В данном случе x и y это верхний левый угол
            double xPos = 0.0;
            double yPos = 1.0;
            for (int i = 0; i < TexturePoints.Length; i += NumberVertex)
            {
                TexturePoints[i] = new(double.Round(xPos, digits), double.Round(yPos, digits));
                TexturePoints[i+1] = new(double.Round(xPos + UnitWidth, digits), double.Round(yPos, 5));
                TexturePoints[i+2] = new(double.Round(xPos + UnitWidth, digits), double.Round(yPos - UnitHeidth, digits));
                TexturePoints[i+3] = new(double.Round(xPos, digits), double.Round(yPos - UnitHeidth, digits));
                if ((i / NumberVertex + 1) % Width == 0 && i != 0)
                {
                    xPos = 0.0;
                    yPos = double.Round(yPos - UnitHeidth, digits);
                }
                else
                {
                    xPos = double.Round(xPos + UnitWidth, digits);
                }
            }
        }

        /// <summary>
        /// Получить текстурные координаты для объекта по индексу текстуры в текстурной карте
        /// </summary>
        /// <param name="index">Индекс текстуры в текстурной карте</param>
        /// <returns></returns>
        public TexturePoint[] GetTexturePoints(int index)
        {
            TexturePoint[] texturePoints = new TexturePoint[4];
            for (int i = index * NumberVertex, j = 0; j < texturePoints.Length; i++, j++)
            {
                texturePoints[j] = TexturePoints[i];
            }
            return texturePoints;
        }
    }
}
