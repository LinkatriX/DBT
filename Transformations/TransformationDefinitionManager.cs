using DBT.Dynamicity;
using DBT.Transformations.Developers.Webmilio;
using DBT.Transformations.LSSJs.Wrathful;
using DBT.Transformations.LSSJs.SSJCType;
using DBT.Transformations.LSSJs.LSSJ;
using DBT.Transformations.Patreon.SSJ5;
using DBT.Transformations.SSJGs.SSJBs.SSJB;
using DBT.Transformations.SSJGs.SSJBs.SSJBE;
using DBT.Transformations.SSJGs.SSJG;
using DBT.Transformations.SSJGs.SSJR;
using DBT.Transformations.SSJs.SSJ1s.ASSJ1;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;
using DBT.Transformations.SSJs.SSJ1s.USSJ1;
using DBT.Transformations.SSJs.SSJ2;
using DBT.Transformations.SSJs.SSJ3;
using DBT.Transformations.SSJs.SSJ4s.SSJ4;
using DBT.Transformations.SSJs.SSJ4s.SSJ4FP;
using DBT.Transformations.Mystics.Mystic;
using DBT.Transformations.Mystics.AwakenedMystic;
using WebmilioCommons.Managers;
using DBT.Transformations.Kaiokens.Kaioken;
using DBT.Transformations.Kaiokens.SSJKK;
using DBT.Transformations.Patreon.DemonSSJ;
using DBT.Transformations.Patreon.DivineSSJ;
using DBT.Transformations.Patreon.LSSJG;

namespace DBT.Transformations
{
    public sealed class TransformationDefinitionManager : SingletonManager<TransformationDefinitionManager, TransformationDefinition> 
    {
        public override void DefaultInitialize()
        {
            Kaioken2x = Add(new Kaioken2XTransformation()) as Kaioken2XTransformation;
            Kaioken3x = Add(new Kaioken3XTransformation(Kaioken2x)) as Kaioken3XTransformation;
            Kaioken4x = Add(new Kaioken4XTransformation(Kaioken3x)) as Kaioken4XTransformation;
            Kaioken10x = Add(new Kaioken10XTransformation(Kaioken4x)) as Kaioken10XTransformation;
            Kaioken20x = Add(new Kaioken20XTransformation(Kaioken10x)) as Kaioken20XTransformation;

            SSJ1 = Add(new SSJ1Transformation()) as SSJ1Transformation;
            ASSJ1 = Add(new ASSJ1Transformation(SSJ1)) as ASSJ1Transformation;
            USSJ1 = Add(new USSJ1Transformation(ASSJ1)) as USSJ1Transformation;
            
            SSJ2 = Add(new SSJ2Transformation(SSJ1)) as SSJ2Transformation;
            SSJKK = Add(new SSJKKTransformation(SSJ1)) as SSJKKTransformation;
            SSJ3 = Add(new SSJ3Transformation(SSJ2)) as SSJ3Transformation;

            SSJ4 = Add(new SSJ4Transformation(SSJ3)) as SSJ4Transformation;
            SSJ4FP = Add(new SSJ4FPTransformation(SSJ4)) as SSJ4FPTransformation;

            SSJ5 = Add(new SSJ5Transformation(SSJ4FP)) as SSJ5Transformation;

            SSJG = Add(new SSJGTransformation(SSJ3)) as SSJGTransformation;
            SSJB = Add(new SSJBTransformation(SSJG)) as SSJBTransformation;
            SSJR = Add(new SSJRTransformation(SSJG)) as SSJRTransformation;
            SSJBE = Add(new SSJBETransformation(SSJB)) as SSJBETransformation;

            Wrathful = Add(new WrathfulTransformation()) as WrathfulTransformation;
            SSJC = Add(new SSJCTypeTransformation(Wrathful)) as SSJCTypeTransformation;
            LSSJ = Add(new LSSJTransformation(SSJC)) as LSSJTransformation;

            Mystic = Add(new MysticTransformation()) as MysticTransformation;
            AwakenedMystic = Add(new AwakenedMysticTransformation(Mystic)) as AwakenedMysticTransformation;

            SoulStealer = Add(new SoulStealerTransformation()) as SoulStealerTransformation;

            DemonSSJ = Add(new DemonSSJTransformation()) as DemonSSJTransformation;

            DivineSSJ = Add(new DivineSSJTransformation()) as DivineSSJTransformation;

            LSSG = Add(new LSSGTransformation()) as LSSGTransformation;

            Tree = new Tree<TransformationDefinition>(byIndex);

            base.DefaultInitialize(); 
        }

        /*public Tree<TransformationDefinition> GenerateConditionalTree(Predicate<TransformationDefinition> condition)
        {

        }*/

        public Kaioken2XTransformation Kaioken2x { get; private set; }
        public Kaioken3XTransformation Kaioken3x { get; private set; }
        public Kaioken4XTransformation Kaioken4x { get; private set; }
        public Kaioken10XTransformation Kaioken10x { get; private set; }
        public Kaioken20XTransformation Kaioken20x { get; private set; }

        public SSJ1Transformation SSJ1 { get; private set; }
        public ASSJ1Transformation ASSJ1 { get; private set; }
        public USSJ1Transformation USSJ1 { get; private set; }
        public SSJKKTransformation SSJKK { get; private set; }

        public SSJ2Transformation SSJ2 { get; private set; }
        public SSJ3Transformation SSJ3 { get; private set; }

        public SSJ4Transformation SSJ4 { get; private set; }
        public SSJ4FPTransformation SSJ4FP { get; private set; }

        public SSJGTransformation SSJG { get; private set; }
        public SSJBTransformation SSJB { get; private set; }
        public SSJRTransformation SSJR { get; private set; }
        public SSJBETransformation SSJBE { get; private set; }

        public WrathfulTransformation Wrathful { get; private set; }
        public SSJCTypeTransformation SSJC { get; private set; }
        public LSSJTransformation LSSJ { get; private set; }

        public MysticTransformation Mystic { get; private set; }
        public AwakenedMysticTransformation AwakenedMystic { get; private set; }

        public SoulStealerTransformation SoulStealer { get; private set; }

        public SSJ5Transformation SSJ5 { get; private set; }

        public DemonSSJTransformation DemonSSJ { get; private set; }

        public DivineSSJTransformation DivineSSJ { get; private set; }

        public LSSGTransformation LSSG { get; private set; }

        public Tree<TransformationDefinition> Tree { get; private set; }
    }
}
