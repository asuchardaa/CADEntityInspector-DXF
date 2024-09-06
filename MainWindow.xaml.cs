using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace DxfLoad_Layers_Dialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region --PARAMETRY--

        private DxfDocument dxfDocument;
        private protected string filePath;

        #endregion --PARAMETRY--

        #region --METODY--

        private void LoadDxfFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "DXF files (*.dxf)|*.dxf",
                Title = "Vyberte DXF soubor"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                dxfDocument = DxfDocument.Load(filePath);

                if (dxfDocument == null)
                {
                    MessageBox.Show("Nepodařilo se načíst DXF soubor.");
                    return;
                }
                LoadLayersIntoListBox();
            }
        }

        private void LoadLayersIntoListBox()
        {
            LayerListBox.Items.Clear();

            foreach (var layer in dxfDocument.Layers)
            {
                LayerListBox.Items.Add(layer.Name);
            }
        }

        private void LoadEntityTypesIntoListBox()
        {
            EntityTypeListBox.Items.Clear();

            var entityTypes = new HashSet<string>();

            if (dxfDocument.Entities.Lines.Any()) entityTypes.Add("Lines");
            if (dxfDocument.Entities.Meshes.Any()) entityTypes.Add("Meshes");
            if (dxfDocument.Entities.MLines.Any()) entityTypes.Add("MLines");
            if (dxfDocument.Entities.MTexts.Any()) entityTypes.Add("MTexts");
            if (dxfDocument.Entities.Points.Any()) entityTypes.Add("Points");
            if (dxfDocument.Entities.PolyfaceMeshes.Any()) entityTypes.Add("PolyfaceMeshes");
            if (dxfDocument.Entities.PolygonMeshes.Any()) entityTypes.Add("PolygonMeshes");
            if (dxfDocument.Entities.Polylines2D.Any()) entityTypes.Add("Polylines2D");
            if (dxfDocument.Entities.Polylines3D.Any()) entityTypes.Add("Polylines3D");
            if (dxfDocument.Entities.Shapes.Any()) entityTypes.Add("Shapes");
            if (dxfDocument.Entities.Solids.Any()) entityTypes.Add("Solids");
            if (dxfDocument.Entities.Splines.Any()) entityTypes.Add("Splines");
            if (dxfDocument.Entities.Texts.Any()) entityTypes.Add("Texts");
            if (dxfDocument.Entities.Rays.Any()) entityTypes.Add("Rays");
            if (dxfDocument.Entities.Underlays.Any()) entityTypes.Add("Underlays");
            if (dxfDocument.Entities.Viewports.Any()) entityTypes.Add("Viewports");
            if (dxfDocument.Entities.Wipeouts.Any()) entityTypes.Add("Wipeouts");
            if (dxfDocument.Entities.XLines.Any()) entityTypes.Add("XLines");

            foreach (var type in entityTypes)
            {
                EntityTypeListBox.Items.Add(type);
            }
        }

        private void ClearEntityList()
        {
            EntityTypeListBox.Items.Clear();
        }

        private void LayersListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LayerListBox.SelectedItem != null)
            {
                string selectedLayer = LayerListBox.SelectedItem.ToString();
                ShowLayerInfo(selectedLayer);
                ClearEntityList();
                UpdateEntityTypesForLayer(selectedLayer);
            }
        }

        private void EntityTypeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LayerListBox.SelectedItem != null && EntityTypeListBox.SelectedItem != null)
            {
                string selectedLayer = LayerListBox.SelectedItem.ToString();
                string selectedEntityType = EntityTypeListBox.SelectedItem.ToString();
                ShowEntityDetails(selectedLayer, selectedEntityType);
            }
        }

        private void UpdateEntityTypesForLayer(string layerName)
        {
            EntityTypeListBox.Items.Clear();
            var entityTypes = new HashSet<string>();

            if (dxfDocument.Entities.Arcs.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Arcs");
            if (dxfDocument.Entities.AttributeDefinitions.Any(e => e.Layer.Name == layerName)) entityTypes.Add("AttributeDefinitions");
            if (dxfDocument.Entities.Circles.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Circles");
            if (dxfDocument.Entities.Dimensions.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Dimensions");
            if (dxfDocument.Entities.Ellipses.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Ellipses");
            if (dxfDocument.Entities.Hatches.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Hatches");
            if (dxfDocument.Entities.Images.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Images");
            if (dxfDocument.Entities.Inserts.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Inserts");
            if (dxfDocument.Entities.Leaders.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Leaders");
            if (dxfDocument.Entities.Lines.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Lines");
            if (dxfDocument.Entities.MLines.Any(e => e.Layer.Name == layerName)) entityTypes.Add("MLines");
            if (dxfDocument.Entities.MTexts.Any(e => e.Layer.Name == layerName)) entityTypes.Add("MTexts");
            if (dxfDocument.Entities.Meshes.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Meshes");
            if (dxfDocument.Entities.Points.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Points");
            if (dxfDocument.Entities.PolyfaceMeshes.Any(e => e.Layer.Name == layerName)) entityTypes.Add("PolyfaceMeshes");
            if (dxfDocument.Entities.PolygonMeshes.Any(e => e.Layer.Name == layerName)) entityTypes.Add("PolygonMeshes");
            if (dxfDocument.Entities.Polylines2D.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Polylines2D");
            if (dxfDocument.Entities.Polylines3D.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Polylines3D");
            if (dxfDocument.Entities.Shapes.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Shapes");
            if (dxfDocument.Entities.Solids.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Solids");
            if (dxfDocument.Entities.Splines.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Splines");
            if (dxfDocument.Entities.Texts.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Texts");
            if (dxfDocument.Entities.Rays.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Rays");
            if (dxfDocument.Entities.Underlays.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Underlays");
            if (dxfDocument.Entities.Viewports.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Viewports");
            if (dxfDocument.Entities.Wipeouts.Any(e => e.Layer.Name == layerName)) entityTypes.Add("Wipeouts");
            if (dxfDocument.Entities.XLines.Any(e => e.Layer.Name == layerName)) entityTypes.Add("XLines");

            foreach (var type in entityTypes)
            {
                EntityTypeListBox.Items.Add(type);
            }
        }

        private void ShowEntityDetails(string layerName, string entityType)
        {
            var info = new StringBuilder();

            IEnumerable<EntityObject> entities = Enumerable.Empty<EntityObject>();

            if (entityType == "Lines")
            {
                entities = dxfDocument.Entities.Lines
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "Meshes")
            {
                entities = dxfDocument.Entities.Meshes
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "MTexts")
            {
                entities = dxfDocument.Entities.MTexts
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "PolygonMeshes")
            {
                entities = dxfDocument.Entities.PolygonMeshes
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "Polylines2D")
            {
                entities = dxfDocument.Entities.Polylines2D
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "Solids")
            {
                entities = dxfDocument.Entities.Solids
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }
            else if (entityType == "Texts")
            {
                entities = dxfDocument.Entities.Texts
                            .Where(e => e.Layer.Name == layerName)
                            .Cast<EntityObject>();
            }

            Debug.WriteLine($"Entity Type: {entityType}");
            Debug.WriteLine($"Layer Name: {layerName}");
            Debug.WriteLine($"Entities Count: {entities.Count()}");

            if (!entities.Any())
            {
                info.AppendLine($"Žádné entity typu '{entityType}' na vrstvě '{layerName}'.\n");
            }
            else
            {
                foreach (var entity in entities)
                {
                    if (entity is Line line)
                    {
                        info.AppendLine($"Line from ({line.StartPoint.X}, {line.StartPoint.Y}, {line.StartPoint.Z}) " +
                                        $"to ({line.EndPoint.X}, {line.EndPoint.Y}, {line.EndPoint.Z})\n");
                    }
                    else if (entity is Mesh mesh)
                    {
                        if (mesh.Vertexes != null)
                        {
                            info.AppendLine($"Mesh with {mesh.Vertexes.Count} vertices\n");
                        }
                        else
                        {
                            info.AppendLine($"Mesh data not available\n");
                        }
                    }
                    else if (entity is PolygonMesh polygonMesh)
                    {
                        if (polygonMesh.Vertexes != null)
                        {
                            info.AppendLine($"PolygonMesh with {polygonMesh.Vertexes.Count()} vertices\n");
                            foreach (var vertex in polygonMesh.Vertexes)
                            {
                                info.AppendLine($"\tVertex: ({vertex.X}, {vertex.Y})\n");
                            }
                        }
                        else
                        {
                            info.AppendLine($"Mesh data not available\n");
                        }
                    }
                    else if (entity is Polyline2D polyline2D)
                    {
                        if (polyline2D.Vertexes != null)
                        {
                            info.AppendLine($"Polyline2D with {polyline2D.Vertexes.Count} vertices:\n");
                            foreach (var vertex in polyline2D.Vertexes)
                            {
                                info.AppendLine($"\tVertex: ({vertex.Position.X}, {vertex.Position.Y})\n");
                            }
                        }
                        else
                        {
                            info.AppendLine($"Polyline2D data not available\n");
                        }
                    }
                    else if (entity is Solid solid)
                    {
                        var vertices = new List<Vector2>
                        {
                            solid.FirstVertex,
                            solid.SecondVertex,
                            solid.ThirdVertex,
                            solid.FourthVertex
                        };

                        // Odstranění nulových hodnot z vrcholů
                        vertices = vertices.Where(v => v != null).ToList();

                        if (vertices.Count > 0)
                        {
                            info.AppendLine($"Solid with {vertices.Count} vertices:\n");
                            foreach (var vertex in vertices)
                            {
                                info.AppendLine($"\tVertex: ({vertex.X}, {vertex.Y})\n");
                            }
                        }
                        else
                        {
                            info.AppendLine($"Solid data is incomplete. Found only {vertices.Count} vertices.\n");
                        }
                    }

                    else if (entity is MText MText)
                    {
                        info.AppendLine($"Mtext with content: {MText.Value}\n");
                    }
                    else if (entity is Text text)
                    {
                        info.AppendLine($"Text with content: {text.Value}\n");
                    }
                }
            }

            EntityDetailsTextBlock.Text = info.ToString();
        }

        private void ShowLayerInfo(string layerName)
        {
            var layerInfo = new StringBuilder();

            layerInfo.AppendLine($"Název souboru: {filePath}\n");
            layerInfo.AppendLine($"Verze souboru: {dxfDocument.DrawingVariables.AcadVer}\n");
            layerInfo.AppendLine($"Komentářů v souboru: {dxfDocument.Comments.Count}\n");

            foreach (var comment in dxfDocument.Comments)
            {
                layerInfo.AppendLine($"\t{comment}\n");
            }

            // Počet entit na vybrané vrstvě
            int arcCount = dxfDocument.Entities.Arcs.Count(e => e.Layer.Name == layerName);
            int attributeDefinitionCount = dxfDocument.Entities.AttributeDefinitions.Count(e => e.Layer.Name == layerName);
            int circleCount = dxfDocument.Entities.Circles.Count(e => e.Layer.Name == layerName);
            int dimensionCount = dxfDocument.Entities.Dimensions.Count(e => e.Layer.Name == layerName);
            int ellipseCount = dxfDocument.Entities.Ellipses.Count(e => e.Layer.Name == layerName);
            int hatchCount = dxfDocument.Entities.Hatches.Count(e => e.Layer.Name == layerName);
            int imageCount = dxfDocument.Entities.Images.Count(e => e.Layer.Name == layerName);
            int insertCount = dxfDocument.Entities.Inserts.Count(e => e.Layer.Name == layerName);
            int leaderCount = dxfDocument.Entities.Leaders.Count(e => e.Layer.Name == layerName);
            int lineCount = dxfDocument.Entities.Lines.Count(e => e.Layer.Name == layerName);
            int mlineCount = dxfDocument.Entities.MLines.Count(e => e.Layer.Name == layerName);
            int mtextCount = dxfDocument.Entities.MTexts.Count(e => e.Layer.Name == layerName);
            int meshCount = dxfDocument.Entities.Meshes.Count(e => e.Layer.Name == layerName);
            int pointCount = dxfDocument.Entities.Points.Count(e => e.Layer.Name == layerName);
            int polyfaceMeshCount = dxfDocument.Entities.PolyfaceMeshes.Count(e => e.Layer.Name == layerName);
            int polygonMeshCount = dxfDocument.Entities.PolygonMeshes.Count(e => e.Layer.Name == layerName);
            int polyline2DCount = dxfDocument.Entities.Polylines2D.Count(e => e.Layer.Name == layerName);
            int polyline3DCount = dxfDocument.Entities.Polylines3D.Count(e => e.Layer.Name == layerName);
            int shapeCount = dxfDocument.Entities.Shapes.Count(e => e.Layer.Name == layerName);
            int solidCount = dxfDocument.Entities.Solids.Count(e => e.Layer.Name == layerName);
            int splineCount = dxfDocument.Entities.Splines.Count(e => e.Layer.Name == layerName);
            int textCount = dxfDocument.Entities.Texts.Count(e => e.Layer.Name == layerName);
            int rayCount = dxfDocument.Entities.Rays.Count(e => e.Layer.Name == layerName);
            int underlayCount = dxfDocument.Entities.Underlays.Count(e => e.Layer.Name == layerName);
            int viewportCount = dxfDocument.Entities.Viewports.Count(e => e.Layer.Name == layerName);
            int wipeoutCount = dxfDocument.Entities.Wipeouts.Count(e => e.Layer.Name == layerName);
            int xlineCount = dxfDocument.Entities.XLines.Count(e => e.Layer.Name == layerName);

            int entitiesCount = arcCount + attributeDefinitionCount +
                                circleCount + dimensionCount + ellipseCount + hatchCount +
                                imageCount + insertCount + leaderCount + lineCount + mlineCount +
                                mtextCount + meshCount + pointCount + polyfaceMeshCount +
                                polygonMeshCount + polyline2DCount + polyline3DCount + shapeCount + solidCount +
                                splineCount + textCount + rayCount + underlayCount + viewportCount + wipeoutCount +
                                xlineCount;

            layerInfo.AppendLine($"\nInformace o vrstvě '{layerName}':\n");
            layerInfo.AppendLine($"Počet entit na vrstvě: {entitiesCount}\n");
            layerInfo.AppendLine($"\tArc; count: {arcCount}\n");
            layerInfo.AppendLine($"\tAttributeDefinition; count: {attributeDefinitionCount}\n");
            layerInfo.AppendLine($"\tCircle; count: {circleCount}\n");
            layerInfo.AppendLine($"\tDimension; count: {dimensionCount}\n");
            layerInfo.AppendLine($"\tEllipse; count: {ellipseCount}\n");
            layerInfo.AppendLine($"\tHatch; count: {hatchCount}\n");
            layerInfo.AppendLine($"\tImage; count: {imageCount}\n");
            layerInfo.AppendLine($"\tInsert; count: {insertCount}\n");
            layerInfo.AppendLine($"\tLeader; count: {leaderCount}\n");
            layerInfo.AppendLine($"\tLine; count: {lineCount}\n");
            layerInfo.AppendLine($"\tMLine; count: {mlineCount}\n");
            layerInfo.AppendLine($"\tMText; count: {mtextCount}\n");
            layerInfo.AppendLine($"\tMesh; count: {meshCount}\n");
            layerInfo.AppendLine($"\tPoint; count: {pointCount}\n");
            layerInfo.AppendLine($"\tPolyfaceMesh; count: {polyfaceMeshCount}\n");
            layerInfo.AppendLine($"\tPolygonMesh; count: {polygonMeshCount}\n");
            layerInfo.AppendLine($"\tPolyline2D; count: {polyline2DCount}\n");
            layerInfo.AppendLine($"\tPolyline3D; count: {polyline3DCount}\n");
            layerInfo.AppendLine($"\tShape; count: {shapeCount}\n");
            layerInfo.AppendLine($"\tSolid; count: {solidCount}\n");
            layerInfo.AppendLine($"\tSpline; count: {splineCount}\n");
            layerInfo.AppendLine($"\tText; count: {textCount}\n");
            layerInfo.AppendLine($"\tRay; count: {rayCount}\n");
            layerInfo.AppendLine($"\tUnderlay; count: {underlayCount}\n");
            layerInfo.AppendLine($"\tViewport; count: {viewportCount}\n");
            layerInfo.AppendLine($"\tWipeout; count: {wipeoutCount}\n");
            layerInfo.AppendLine($"\tXLine; count: {xlineCount}\n");

            LayerInfoTextBlock.Text = layerInfo.ToString();
        }

        #endregion --METODY--
    }
}
