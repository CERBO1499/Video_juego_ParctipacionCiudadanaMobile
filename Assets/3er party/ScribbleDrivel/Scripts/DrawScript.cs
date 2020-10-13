using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LylekGames
{
    public class DrawScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public static DrawScript drawScript;

        //              ---------------------------------------------ASDIUS INICIO      -------------------------------------

        //  [SerializeField] GameObject botonSiguiente;

        //              ----------------------------------------------Asdius FIN    -------------------------------

        public bool optimize;
        public Canvas canvas;
        public GameObject brushPrefab;
        private Image brush;
        private Image ourImage;

        public Color brushColor = Color.black;
        [Range(10, 50)]
        public int brushSize = 10;
        [Range(0.1f, 1.0f)]
        public float spacing = 0.25f;

        //used to draw a line from our previous mouse pos to our current
        private Vector3 previousMousePosition = Vector3.zero;
        private bool drawingInProgress = false;
        public bool canDraw = false;

        //Used to position our brush tool
        private GameObject brushHolder;
        //Used to store our drawing history
        private GameObject historyHolder;
        //Used to store our current drawing
        private GameObject dotField;
        //store our draw history
        public List<GameObject> drawHistory = new List<GameObject>();

        public void Awake()
        {
            drawScript = this;
        }
        public void GetDefaultSettings()
        {
            optimize = false;
            if (!GetComponent<Mask>())
                gameObject.AddComponent<Mask>();
            brushPrefab = Resources.Load("Brush") as GameObject;
            if (!brushPrefab)
                Debug.LogError("Cannot locate Brush prefab. Please assign a brush to the DrawScript, in the Inspector. This may be a simple UI element containing an Image Component.");
            if(this.GetComponent<Image>())
                ourImage = this.GetComponent<Image>();
            if (!canvas)
                if (transform.root.gameObject.GetComponent<Canvas>())
                    canvas = transform.root.gameObject.GetComponent<Canvas>();
            brushColor = Color.black;
            brushSize = 10;
            spacing = 0.25f;
            previousMousePosition = Vector3.zero;
            canDraw = false;
        }
        public void Start()
        {
           // botonSiguiente.SetActive(false);
            if (this.GetComponent<Image>())
                ourImage = this.GetComponent<Image>();
            if (!canvas)
                if (transform.root.gameObject.GetComponent<Canvas>())
                    canvas = transform.root.gameObject.GetComponent<Canvas>();
            if (!historyHolder)
                historyHolder = NewUIObject("DrawHistory");
            if (!brushHolder)
                brushHolder = NewUIObject("BrushHolder");
            if (!brushPrefab)
                brushPrefab = Resources.Load("Brush") as GameObject;
            if (brushPrefab)
            {
                brush = Instantiate(brushPrefab.gameObject, Input.mousePosition, Quaternion.identity).GetComponent<Image>();
                brush.gameObject.transform.SetParent(brushHolder.transform, false);

                SetBrushSize(brushSize);
                SetBrushColor(brushColor);
            }
            else
                Debug.LogError("Brush is missing. Please assign a brush to the DrawScript, in the Inspector. This may be a simple gameObject containing an Image Component.");
        }
        public GameObject NewUIObject(string name)
        {
            GameObject newObject = new GameObject(name);
            newObject.AddComponent<CanvasRenderer>();
            newObject.transform.SetParent(this.gameObject.transform, false);
            newObject.transform.rotation = Quaternion.identity;

            RectTransform rectTrans = this.GetComponent<RectTransform>();
            RectTransform newRectTrans = newObject.AddComponent<RectTransform>();
            newRectTrans.pivot = rectTrans.pivot;
            newRectTrans.anchorMin = rectTrans.anchorMin;
            newRectTrans.anchorMax = rectTrans.anchorMax;
            newRectTrans.sizeDelta = rectTrans.sizeDelta;
            newRectTrans.position = rectTrans.position;
            newObject.hideFlags = HideFlags.HideInHierarchy;
            return newObject;
        }
        public void SetBrushSize(int bSize)
        {
            brushSize = bSize;
            Vector2 newBrushSize = new Vector2(brushSize, brushSize);
            brush.rectTransform.sizeDelta = newBrushSize;
        }
        public void SetBrushColor(Color bColor)
        {
            brushColor = bColor;
            brush.color = brushColor;
        }
        public void SetBrushShape(Sprite bSprite)
        {
            brush.sprite = bSprite;
        }
        public void Undo()
        {
            if (drawHistory.Count > 0)
            {
                Destroy(drawHistory[drawHistory.Count - 1].gameObject);
                drawHistory.RemoveAt(drawHistory.Count - 1);
            }
         //   else botonSiguiente.SetActive(false);
        }
        //AGREGADO ASDIUS                               ---------------------------------------------------------------------
        public void Resett()
        {
            if (drawHistory.Count > 0)
            {
                int cantidad = drawHistory.Count;
                for (int i = 0; i < cantidad; i++)
                {
                    Destroy(drawHistory[drawHistory.Count - 1].gameObject);
                    drawHistory.RemoveAt(drawHistory.Count - 1);
                }
               // botonSiguiente.SetActive(false);
            }
            
            
        }
        //FINAL AGREGADO ASDIUS                         ---------------------------------------------------------------------
        public void OnPointerEnter(PointerEventData eventData)
        {
            canDraw = true;
            brush.gameObject.SetActive(true);
           // botonSiguiente.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            canDraw = false;
            brush.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (canDraw)
            {
                if (brush)
                {
                    if (canvas && canvas.renderMode == RenderMode.WorldSpace || canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera && canvas.worldCamera != null)
                    {
                        Plane plane = new Plane();
                        plane.Set3Points(canvas.transform.TransformPoint(new Vector3(0, 0, 0)), canvas.transform.TransformPoint(new Vector3(0, 1, 0)), canvas.transform.TransformPoint(new Vector3(1, 0, 0)));

                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        float rayHitDistance;

                        if (plane.Raycast(ray, out rayHitDistance))
                        {
                            brush.transform.position = ray.GetPoint(rayHitDistance);
                            brush.transform.rotation = canvas.transform.rotation;
                        }
                    }
                    else
                    {
                        brush.transform.position = Input.mousePosition;
                        if (!canvas)
                            Debug.LogWarning("No canvas found. If your canvas is rendered in World Space results may very if the canvas is left unassigned.");
                    }
                }
                else
                    Debug.LogError("Brush is missing. Please assign a brush to the DrawScript, in the Inspector. This may be a simple gameObject containing an Image Component.");
                if (Input.GetMouseButtonUp(0))
                {
                    brush.gameObject.SetActive(true);
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    SetBrushSize(brushSize);

                    //Create dotField
                    if (optimize)
                    {
                        dotField = NewUIObject("dotField");
                        dotField.transform.SetParent(historyHolder.transform, false);
                    }
                    else
                    {
                        //Create history
                        dotField = NewUIObject("drawHist " + drawHistory.Count);
                        dotField.transform.SetParent(historyHolder.transform, false);
                        drawHistory.Add(dotField);
                    }

                    previousMousePosition = brush.transform.position;
                    //Draw a dot
                    Draw(brush.transform.position);
                }
                else if (Input.GetMouseButton(0))
                {
                    if(!dotField)
                    {
                        //Create dotField
                        if (optimize)
                        {
                            dotField = NewUIObject("dotField");
                            dotField.transform.SetParent(historyHolder.transform, false);
                        }
                        else
                        {
                            //Create history
                            dotField = NewUIObject("drawHist " + drawHistory.Count);
                            dotField.transform.SetParent(historyHolder.transform, false);
                            drawHistory.Add(dotField);
                        }
                    }
                    if (previousMousePosition != brush.transform.position)
                    {
                        if (drawingInProgress == false)
                            DrawDistance(previousMousePosition, brush.transform.position);
                    }
                }
            }
            else
            {
                //Check to see if our cursor if over our drawing board (in case the PointerHandlers miss it)
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
                if (hit)
                {
                    if (hit.transform.gameObject == this.gameObject)
                        canDraw = true;
                }
            }
            //Optimize Image
            if (Input.GetMouseButtonUp(0))
            {
                if(optimize)
                    if(dotField)
                        StartCoroutine(OptimizeImage());
            }
        }
        private void DrawDistance(Vector3 oldPos, Vector3 newPos)
        {
            drawingInProgress = true;
            float spaceDist = Vector3.Distance(oldPos, newPos);
            float actualSpacing = spacing * brush.rectTransform.rect.height;
            float newSpace = actualSpacing / spaceDist;
            float lerpStep = 0;
            if (spaceDist >= actualSpacing)
            {
                while (lerpStep <= 1)
                {
                    Vector3 newDotPos = Vector3.Lerp(oldPos, newPos, lerpStep);
                    Draw(newDotPos);

                    lerpStep += newSpace;
                }
                previousMousePosition = newPos;
            }
            drawingInProgress = false;
        }
        private void Draw(Vector3 pos)
        {
            GameObject newDot = Instantiate(brush.gameObject) as GameObject;
            newDot.transform.position = pos;
            newDot.SetActive(true);
            newDot.transform.SetParent(dotField.transform, true);
        }
        public IEnumerator OptimizeImage()
        {
            brush.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();
            
            Vector3[] ourCoordinates = new Vector3[4];
            ourImage.rectTransform.GetWorldCorners(ourCoordinates);
            Vector3[] canvasCoordinates = new Vector3[4];
            canvas.GetComponent<RectTransform>().GetWorldCorners(canvasCoordinates);
            float posX = ourCoordinates[0].x / canvasCoordinates[3].x;
            float posY = ourCoordinates[0].y / canvasCoordinates[1].y;
            float posW = (ourCoordinates[3].x - ourCoordinates[0].x) / canvasCoordinates[3].x;
            float posH = (ourCoordinates[1].y - ourCoordinates[0].y) / canvasCoordinates[1].y;

            Vector2 actualPos = new Vector2(posX * Screen.width, posY * Screen.height);
            Vector2 actualSize = new Vector2(posW * Screen.width, posH * Screen.height);

            Texture2D screenShot = new Texture2D((int)actualSize.x, (int)actualSize.y, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(actualPos.x + 1, actualPos.y + 1, (int)actualSize.x, (int)actualSize.y), 0, 0);
            screenShot.Apply();

            Sprite tempSprite = Sprite.Create(screenShot, new Rect(0, 0, (int)actualSize.x, (int)actualSize.y), Vector2.zero);

            GameObject newDrawHist = NewUIObject("drawHist " + drawHistory.Count);
            Image newImage = newDrawHist.AddComponent<Image>();
            newImage.sprite = tempSprite;
            newImage.transform.SetParent(historyHolder.transform, false);
            drawHistory.Add(newDrawHist.gameObject);

            if(canDraw)
                brush.gameObject.SetActive(true);
            if(dotField)
                Destroy(dotField);
        }
    }
}
