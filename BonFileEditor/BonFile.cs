using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonFileEditor
{
    public class BonFile
    {
        public long Header;
        public int Version;
        public int count;
        public int count2;
        public int count3;
        public int unknown0;
        public int spacecontrol;
        public int unknown;
        public byte[] Empty;
        public List<FirstStage> FirstStages = new List<FirstStage>();
        public byte[] Empty2;
        public List<SecondStage> SecondStages = new List<SecondStage>();
        public List<SecondStageAlt> SecondStagesAlt = new List<SecondStageAlt>();
        public List<FinalStage> GfxAnimations = new List<FinalStage>();
        public BonFile(string file)
        {
            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open));
            Header = br.ReadInt64();
            Version = br.ReadInt32();
            count = br.ReadInt32();
            count2 = br.ReadInt32();
            count3 = br.ReadInt32();
            unknown0 = br.ReadInt32();
            spacecontrol = br.ReadInt32();
            unknown = br.ReadInt32();
            Empty = br.ReadBytes(64);
            for (int i = 0; i < count; i++)
            {
                FirstStages.Add(new FirstStage(br, Version));
            }
            if (Version == 6)
            {
                Empty2 = br.ReadBytes(count2 * 4);
            }
            if (spacecontrol > 7)
            {
                Empty2 = br.ReadBytes(count2 * 4);
            }
            for (int i = 0; i < count2; i++)
            {
                if (Version == 5)
                {
                    SecondStages.Add(new SecondStage(br, Version));
                }
                else
                {
                    SecondStagesAlt.Add(new SecondStageAlt(br, Version));
                }
            }
            for (int i = 0; i < count3; i++)
            {
                GfxAnimations.Add(new FinalStage(br, Version));
            }
            br.Close();
        }
        public void Save(string path, int vers)
        {
            BinaryWriter bw = new BinaryWriter(File.Create(path));
            bw.Write(Header);
            bw.Write(Version);
            bw.Write(FirstStages.Count);
            bw.Write(count2);
            bw.Write(count3);
            bw.Write(unknown0);
            bw.Write(spacecontrol);
            bw.Write(unknown);
            bw.Write(Empty);
            for (int i = 0; i < FirstStages.Count; i++)
            {
                FirstStages[i].Save(bw, vers);
            }
            if(vers==6)
            {
                bw.Write(Empty2);
            }
            if(spacecontrol>7)
            {
                bw.Write(Empty2);
            }
            for (int i = 0; i < count2; i++)
            {
                if(vers==5)
                {
                    SecondStages[i].Save(bw, vers);
                }
                else
                {
                    SecondStagesAlt[i].Save(bw);
                }
            }
            for (int i = 0; i < count3; i++)
            {
                GfxAnimations[i].Save(bw);
            }
            bw.Close();
        }
    }
    public class FirstStage
    {
        public string BoneName;
        public int Extras2;
        public int[] ID = new int[2];
        public Point3D[] Points = new Point3D[8];
        public int unknown0;
        public int unknown1;
        public int unknown2;
        public decimal unknown3;
        public decimal unknown4;
        public decimal unknown5;
        public decimal unknown6;
        public decimal unknown7;
        public List<int> Extras = new List<int>();
        public FirstStage(BinaryReader br, int Version)
        {
            BoneName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            Extras2 = br.ReadInt32();
            ID[0] = br.ReadInt32();
            ID[1] = br.ReadInt32();
            Extras = new List<int>(br.ReadInt32());
            Points[0] = new Point3D(br);
            unknown0 = br.ReadInt32();
            Points[1] = new Point3D(br);
            unknown1 = br.ReadInt32();
            Points[2] = new Point3D(br);
            unknown2 = br.ReadInt32();
            Points[3] = new Point3D(br);
            unknown3 = (decimal)br.ReadSingle();
            Points[4] = new Point3D(br);
            unknown4 = (decimal)br.ReadSingle();
            Points[5] = new Point3D(br);
            unknown5 = (decimal)br.ReadSingle();
            Points[6] = new Point3D(br);
            unknown6 = (decimal)br.ReadSingle();
            Points[7] = new Point3D(br);
            unknown7 = (decimal)br.ReadSingle();
            for (int i = 0; i < Extras.Capacity; i++)
            {
                Extras.Add(br.ReadInt32());
            }
        }
        public void Save(BinaryWriter bw, int vers)
        {
            byte[] b = Encoding.GetEncoding(936).GetBytes(BoneName);
            bw.Write(b.Length);
            bw.Write(b);
            bw.Write(Extras2);
            bw.Write(ID[0]);
            bw.Write(ID[1]);
            bw.Write(Extras.Count);
            Points[0].Save(bw);
            bw.Write(unknown0);
            Points[1].Save(bw);
            bw.Write(unknown1);
            Points[2].Save(bw);
            bw.Write(unknown2);
            Points[3].Save(bw);
            bw.Write((float)unknown3);
            Points[4].Save(bw);
            bw.Write((float)unknown4);
            Points[5].Save(bw);
            bw.Write((float)unknown5);
            Points[6].Save(bw);
            bw.Write((float)unknown6);
            Points[7].Save(bw);
            bw.Write((float)unknown7);
            for (int i = 0; i < Extras.Count; i++)
            {
                bw.Write(Extras[i]);
            }
        }
    }
    public class SecondStage
    {
        public string AnimationName;
        public int unknown1;
        public int unknown2;
        public int unknown3;
        public int cordcount;
        public int cordcount2;
        public int spacetype;
        public Point3D[] SingleCords = new Point3D[2];
        public int unknown4;
        public int unknown5;
        public int unknown6;
        public int unknown7;
        public int unknown8;
        public int unknown9;
        public int unknown10;
        public float unknown11;
        public int unknown12;
        public int unknown13;
        public int unknown14;
        public int unknown15;
        public List<Point3D> Cords = new List<Point3D>();
        public List<Stage2SectionB> Stage2SectionBs = new List<Stage2SectionB>();
        public int cordcount3;
        public int cordcount4;
        public int unknown16;
        public List<BuchaFloats> BuchaFloats = new List<BuchaFloats>();
        public List<Stage2SectionB> Stage2SectionBs2 = new List<Stage2SectionB>();
        public SecondStage(BinaryReader br, int Version)
        {
            AnimationName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            unknown1 = br.ReadInt32();
            unknown2 = br.ReadInt32();
            unknown3 = br.ReadInt32();
            cordcount = br.ReadInt32();
            cordcount2 = br.ReadInt32();
            spacetype = br.ReadInt32();
            if (spacetype == 31)
            {
                SingleCords[0] = new Point3D(br);
                unknown4 = br.ReadInt32();
                unknown5 = br.ReadInt32();
                unknown6 = br.ReadInt32();
                unknown7 = br.ReadInt32();
                unknown8 = br.ReadInt32();
                unknown9 = br.ReadInt32();
                unknown10 = br.ReadInt32();
                unknown11 = br.ReadSingle();
                SingleCords[1] = new Point3D(br);
                unknown12 = br.ReadInt32();
                unknown13 = br.ReadInt32();
                unknown14 = br.ReadInt32();
                unknown15 = br.ReadInt32();
            }
            else
            {
                for (int i = 0; i < cordcount; i++)
                {
                    Cords.Add(new Point3D(br));
                }
                for (int i = 0; i < cordcount2; i++)
                {
                    Stage2SectionBs.Add(new Stage2SectionB(br));
                }
                cordcount3 = br.ReadInt32();
                cordcount4 = br.ReadInt32();
                unknown16 = br.ReadInt32();
                for (int i = 0; i < cordcount3; i++)
                {
                    BuchaFloats.Add(new BuchaFloats(br));
                }
                for (int i = 0; i < cordcount4; i++)
                {
                    Stage2SectionBs2.Add(new Stage2SectionB(br));
                }
            }
        }
        public void Save(BinaryWriter bw,int vers)
        {
            byte[] b = Encoding.GetEncoding(936).GetBytes(AnimationName);
            bw.Write(b.Length);
            bw.Write(b);
            bw.Write(unknown1);
            bw.Write(unknown2);
            bw.Write(unknown3);
            bw.Write(cordcount);
            bw.Write(cordcount2);
            bw.Write(spacetype);
            if(spacetype==31)
            {
                SingleCords[0].Save(bw);
                bw.Write(unknown4);
                bw.Write(unknown5);
                bw.Write(unknown6);
                bw.Write(unknown7);
                bw.Write(unknown8);
                bw.Write(unknown9);
                bw.Write(unknown10);
                bw.Write(unknown11);
                SingleCords[1].Save(bw);
                bw.Write(unknown12);
                bw.Write(unknown13);
                bw.Write(unknown14);
                bw.Write(unknown15);
            }
            else
            {
                for (int i = 0; i < cordcount; i++)
                {
                    Cords[i].Save(bw);
                }
                for (int i = 0; i < cordcount2; i++)
                {
                    Stage2SectionBs[i].Save(bw);
                }
                bw.Write(cordcount3);
                bw.Write(cordcount4);
                bw.Write(unknown16);
                for (int i = 0; i < cordcount3; i++)
                {
                    BuchaFloats[i].Save(bw);
                }
                for (int i = 0; i < cordcount4; i++)
                {
                    Stage2SectionBs2[i].Save(bw);
                }
            }
        }
    }
    public class SecondStageAlt
    {
        public string BoneName;
        public int[] ID = new int[2];
        public int Unknown1;
        public SecondStageAlt(BinaryReader br, int Version)
        {
            BoneName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            ID[0] = br.ReadInt32();
            ID[1] = br.ReadInt32();
            Unknown1 = br.ReadInt32();
        }
        public void Save(BinaryWriter bw)
        {
            byte[] b = Encoding.GetEncoding(936).GetBytes(BoneName);
            bw.Write(b.Length);
            bw.Write(b);
            bw.Write(ID[0]);
            bw.Write(ID[1]);
            bw.Write(Unknown1);
        }
    }
    public class FinalStage
    {
        public string BoneName;
        public int unknown0;
        public int ID;
        public Point3D[] Cords = new Point3D[4];
        public int[] CordsInts = new int[3];
        public float unknown1;
        public FinalStage(BinaryReader br, int Version)
        {
            BoneName = Encoding.GetEncoding(936).GetString(br.ReadBytes(br.ReadInt32()));
            unknown0 = br.ReadInt32();
            ID = br.ReadInt32();
            Cords[0] = new Point3D(br);
            CordsInts[0] = br.ReadInt32();
            Cords[1] = new Point3D(br);
            CordsInts[1] = br.ReadInt32();
            Cords[2] = new Point3D(br);
            CordsInts[2] = br.ReadInt32();
            Cords[3] = new Point3D(br);
            unknown1 = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            byte[] b = Encoding.GetEncoding(936).GetBytes(BoneName);
            bw.Write(b.Length);
            bw.Write(b);
            bw.Write(unknown0);
            bw.Write(ID);
            Cords[0].Save(bw);
            bw.Write(CordsInts[0]);
            Cords[1].Save(bw);
            bw.Write(CordsInts[1]);
            Cords[2].Save(bw);
            bw.Write(CordsInts[2]);
            Cords[3].Save(bw);
            bw.Write(unknown1);
        }
    }
    public class Point3D
    {
        public decimal X;
        public decimal Y;
        public decimal Z;
        public Point3D(BinaryReader br)
        {
            X = (decimal)br.ReadSingle();
            Y = (decimal)br.ReadSingle();
            Z = (decimal)br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write((float)X);
            bw.Write((float)Y);
            bw.Write((float)Z);
        }
    }
    public class Stage2SectionB
    {
        public int Unknown1;
        public int unknown2;
        public int[] ID = new int[2];
        public Stage2SectionB(BinaryReader br)
        {
            Unknown1 = br.ReadInt32();
            unknown2 = br.ReadInt32();
            ID[0] = br.ReadInt32();
            ID[1] = br.ReadInt32();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(Unknown1);
            bw.Write(unknown2);
            bw.Write(ID[0]);
            bw.Write(ID[1]);
        }
    }
    public class BuchaFloats
    {
        public float unknown;
        public float unknown1;
        public float unknown2;
        public float unknown3;
        public BuchaFloats(BinaryReader br)
        {
            unknown = br.ReadSingle();
            unknown1 = br.ReadSingle();
            unknown2 = br.ReadSingle();
            unknown3 = br.ReadSingle();
        }
        public void Save(BinaryWriter bw)
        {
            bw.Write(unknown);
            bw.Write(unknown1);
            bw.Write(unknown2);
            bw.Write(unknown3);

        }
    }
}
