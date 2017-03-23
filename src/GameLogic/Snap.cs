using System;using SwinGameSDK;



#if DEBUGusing NUnit.Framework;


#endif

namespace CardGames.GameLogic{








	/// <summary>	/// The Snap card game in which the user scores a point if they	/// click when the rank of the last two cards match.	/// </summary>    public class Snap	{
		// Keep only the last two cards...
		private readonly Card [] _topCards = new Card [2];

		// Have a Deck of cards to play with.
		private readonly Deck _deck;

		// Use a timer to allow the game to draw cards at timed intervals
		private readonly Timer _gameTimer;

		// The amount of time that must pass before a card is flipped?
		private int _flipTime = 1000;

		// the score for the 2 players
		private int [] _score = new int [2];		private bool _started = false;







		/// <summary>		/// Create a new game of Snap!		/// </summary>        public Snap ()		{			_deck = new Deck ();			_gameTimer = SwinGame.CreateTimer ();		}









		/// <summary>		/// Gets the card on the top of the "flip" stack. This card will be face up.		/// </summary>		/// <value>The top card.</value>        public Card TopCard {			get {				return _topCards [1];			}		}











		/// <summary>		/// Indicates if there are cards remaining in the Snap game's Deck.		/// The game is over when there are no cards remaining.		/// </summary>		/// <value><c>true</c> if cards remain; otherwise, <c>false</c>.</value>        public bool CardsRemain {			get { return _deck.CardsRemaining > 0; }		}











		/// <summary>		/// Determines how many milliseconds need to pass before a new card is drawn		/// and placed on the top of the game's card stack.		/// </summary>		/// <value>The flip time.</value>        public int FlipTime {			get { return _flipTime; }			set { _flipTime = value; }		}









		/// <summary>		/// Indicates if the game has already been started. You can only start the game once.		/// </summary>		/// <value><c>true</c> if this instance is started; otherwise, <c>false</c>.</value>        public bool IsStarted {			get { return _started; }		}







		/// <summary>		/// Start the Snap game playing!		/// </summary>        public void Start ()		{			if (!IsStarted)         // only start if not already started!
														{				_started = true;				_deck.Shuffle ();       // Return the cards and shuffle

				FlipNextCard ();        // Flip the first card...
				_gameTimer.Start ();			}		}		public void FlipNextCard ()		{			if (_deck.CardsRemaining > 0)           // have cards...
										{				_topCards [0] = _topCards [1];      // move top to card 2
				_topCards [1] = _deck.Draw ();      // get a new top card
				_topCards [1].TurnOver ();          // reveal card
			}		}









		/// <summary>		/// Update the game. This should be called in the Game loop to enable		/// the game to update its internal state.		/// </summary>        public void Update ()		{
			//TODO: implement update to automatically slip cards!
			if (_gameTimer.Ticks > _flipTime) {				_gameTimer.Reset ();				FlipNextCard ();			}		}









		/// <summary>		/// Gets the player's score.		/// </summary>		/// <value>The score.</value>        public int Score (int idx)		{			if (idx >= 0 && idx < _score.Length)				return _score [idx];			else				return 0;		}









		/// <summary>		/// The player hit the top of the cards "snap"! :)		/// Check if the top two cards' ranks match.		/// </summary>        public void PlayerHit (int player)		{
			//TODO: consider deducting score for miss hits???
			if (player >= 0 && player < _score.Length &&    // its a valid player
IsStarted &&                               // and the game is started
			 _topCards [0] != null && _topCards [0].Rank == _topCards [1].Rank) // and its a match
{				_score [player]++;
				//TODO: consider playing a sound here...
			} else if (player >= 0 && player < _score.Length) {				_score [player]--;			}

			// stop the game...
			_started = false;			_gameTimer.Stop ();		}




		#region Snap Game Unit Tests
#if DEBUG
		public class SnapTests		{			[Test]			public void TestSnapCreation ()			{				Snap s = new Snap ();				Assert.IsTrue (s.CardsRemain);				Assert.IsNull (s.TopCard);			}			[Test]			public void TestFlipNextCard ()			{				Snap s = new Snap ();				Assert.IsTrue (s.CardsRemain);				Assert.IsNull (s.TopCard);				s.FlipNextCard ();				Assert.IsNull (s._topCards [0]);				Assert.IsNotNull (s._topCards [1]);			}		}




#endif
		#endregion    }} 
 u s i n g   S w i n G a m e S D K ;  
  
 # i f   D E B U G  
 u s i n g   N U n i t . F r a m e w o r k ;  
 # e n d i f  
  
  
 n a m e s p a c e   C a r d G a m e s . G a m e L o g i c  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   T h e   S n a p   c a r d   g a m e   i n   w h i c h   t h e   u s e r   s c o r e s   a   p o i n t   i f   t h e y  
 	 / / /   c l i c k   w h e n   t h e   r a n k   o f   t h e   l a s t   t w o   c a r d s   m a t c h .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   S n a p  
 	 {  
 	 	 / /   K e e p   o n l y   t h e   l a s t   t w o   c a r d s . . .  
 	 	 p r i v a t e   r e a d o n l y   C a r d   [ ]   _ t o p C a r d s   =   n e w   C a r d   [ 2 ] ;  
  
 	 	 / /   H a v e   a   D e c k   o f   c a r d s   t o   p l a y   w i t h .  
 	 	 p r i v a t e   r e a d o n l y   D e c k   _ d e c k ;  
  
 	 	 / /   U s e   a   t i m e r   t o   a l l o w   t h e   g a m e   t o   d r a w   c a r d s   a t   t i m e d   i n t e r v a l s  
 	 	 p r i v a t e   r e a d o n l y   T i m e r   _ g a m e T i m e r ;  
  
 	 	 / /   T h e   a m o u n t   o f   t i m e   t h a t   m u s t   p a s s   b e f o r e   a   c a r d   i s   f l i p p e d ?  
 	 	 p r i v a t e   i n t   _ f l i p T i m e   =   1 0 0 0 ;  
  
 	 	 / /   t h e   s c o r e   f o r   t h e   2   p l a y e r s  
 	 	 p r i v a t e   i n t   [ ]   _ s c o r e   =   n e w   i n t   [ 2 ] ;  
  
 	 	 p r i v a t e   b o o l   _ s t a r t e d   =   f a l s e ;  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   C r e a t e   a   n e w   g a m e   o f   S n a p !  
 	 	 / / /   < / s u m m a r y >  
 	 	 p u b l i c   S n a p   ( )  
 	 	 {  
 	 	 	 _ d e c k   =   n e w   D e c k   ( ) ;  
 	 	 	 _ g a m e T i m e r   =   S w i n G a m e . C r e a t e T i m e r   ( ) ;  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   c a r d   o n   t h e   t o p   o f   t h e   " f l i p "   s t a c k .   T h i s   c a r d   w i l l   b e   f a c e   u p .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   t o p   c a r d . < / v a l u e >  
 	 	 p u b l i c   C a r d   T o p C a r d   {  
 	 	 	 g e t   {  
 	 	 	 	 r e t u r n   _ t o p C a r d s   [ 1 ] ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   I n d i c a t e s   i f   t h e r e   a r e   c a r d s   r e m a i n i n g   i n   t h e   S n a p   g a m e ' s   D e c k .  
 	 	 / / /   T h e   g a m e   i s   o v e r   w h e n   t h e r e   a r e   n o   c a r d s   r e m a i n i n g .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > < c > t r u e < / c >   i f   c a r d s   r e m a i n ;   o t h e r w i s e ,   < c > f a l s e < / c > . < / v a l u e >  
 	 	 p u b l i c   b o o l   C a r d s R e m a i n   {  
 	 	 	 g e t   {   r e t u r n   _ d e c k . C a r d s R e m a i n i n g   >   0 ;   }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   D e t e r m i n e s   h o w   m a n y   m i l l i s e c o n d s   n e e d   t o   p a s s   b e f o r e   a   n e w   c a r d   i s   d r a w n  
 	 	 / / /   a n d   p l a c e d   o n   t h e   t o p   o f   t h e   g a m e ' s   c a r d   s t a c k .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   f l i p   t i m e . < / v a l u e >  
 	 	 p u b l i c   i n t   F l i p T i m e   {  
 	 	 	 g e t   {   r e t u r n   _ f l i p T i m e ;   }  
 	 	 	 s e t   {   _ f l i p T i m e   =   v a l u e ;   }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   I n d i c a t e s   i f   t h e   g a m e   h a s   a l r e a d y   b e e n   s t a r t e d .   Y o u   c a n   o n l y   s t a r t   t h e   g a m e   o n c e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > < c > t r u e < / c >   i f   t h i s   i n s t a n c e   i s   s t a r t e d ;   o t h e r w i s e ,   < c > f a l s e < / c > . < / v a l u e >  
 	 	 p u b l i c   b o o l   I s S t a r t e d   {  
 	 	 	 g e t   {   r e t u r n   _ s t a r t e d ;   }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   S t a r t   t h e   S n a p   g a m e   p l a y i n g !  
 	 	 / / /   < / s u m m a r y >  
 	 	 p u b l i c   v o i d   S t a r t   ( )  
 	 	 {  
 	 	 	 i f   ( ! I s S t a r t e d )                   / /   o n l y   s t a r t   i f   n o t   a l r e a d y   s t a r t e d !  
 	 	 	 {  
 	 	 	 	 _ s t a r t e d   =   t r u e ;  
 	 	 	 	 _ d e c k . S h u f f l e   ( ) ;               / /   R e t u r n   t h e   c a r d s   a n d   s h u f f l e  
  
 	 	 	 	 F l i p N e x t C a r d   ( ) ;                 / /   F l i p   t h e   f i r s t   c a r d . . .  
 	 	 	 	 _ g a m e T i m e r . S t a r t   ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   F l i p N e x t C a r d   ( )  
 	 	 {  
 	 	 	 i f   ( _ d e c k . C a r d s R e m a i n i n g   >   0 )                       / /   h a v e   c a r d s . . .  
 	 	 	 {  
 	 	 	 	 _ t o p C a r d s   [ 0 ]   =   _ t o p C a r d s   [ 1 ] ;             / /   m o v e   t o p   t o   c a r d   2  
 	 	 	 	 _ t o p C a r d s   [ 1 ]   =   _ d e c k . D r a w   ( ) ;             / /   g e t   a   n e w   t o p   c a r d  
 	 	 	 	 _ t o p C a r d s   [ 1 ] . T u r n O v e r   ( ) ;                     / /   r e v e a l   c a r d  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   U p d a t e   t h e   g a m e .   T h i s   s h o u l d   b e   c a l l e d   i n   t h e   G a m e   l o o p   t o   e n a b l e  
 	 	 / / /   t h e   g a m e   t o   u p d a t e   i t s   i n t e r n a l   s t a t e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 p u b l i c   v o i d   U p d a t e   ( )  
 	 	 {  
 	 	 	 / / T O D O :   i m p l e m e n t   u p d a t e   t o   a u t o m a t i c a l l y   s l i p   c a r d s !  
 	 	 	 i f   ( _ g a m e T i m e r . T i c k s   >   _ f l i p T i m e )   {  
  
 	 	 	 	 _ g a m e T i m e r . R e s e t   ( ) ;  
 	 	 	 	 F l i p N e x t C a r d   ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   G e t s   t h e   p l a y e r ' s   s c o r e .  
 	 	 / / /   < / s u m m a r y >  
 	 	 / / /   < v a l u e > T h e   s c o r e . < / v a l u e >  
 	 	 p u b l i c   i n t   S c o r e   ( i n t   i d x )  
 	 	 {  
 	 	 	 i f   ( i d x   > =   0   & &   i d x   <   _ s c o r e . L e n g t h )  
 	 	 	 	 r e t u r n   _ s c o r e   [ i d x ] ;  
 	 	 	 e l s e  
 	 	 	 	 r e t u r n   0 ;  
 	 	 }  
  
 	 	 / / /   < s u m m a r y >  
 	 	 / / /   T h e   p l a y e r   h i t   t h e   t o p   o f   t h e   c a r d s   " s n a p " !   : )  
 	 	 / / /   C h e c k   i f   t h e   t o p   t w o   c a r d s '   r a n k s   m a t c h .  
 	 	 / / /   < / s u m m a r y >  
 	 	 p u b l i c   v o i d   P l a y e r H i t   ( i n t   p l a y e r )  
 	 	 {  
 	 	 	 / / T O D O :   c o n s i d e r   d e d u c t i n g   s c o r e   f o r   m i s s   h i t s ? ? ?  
 	 	 	 i f   ( p l a y e r   > =   0   & &   p l a y e r   <   _ s c o r e . L e n g t h   & &         / /   i t s   a   v a l i d   p l a y e r  
 	 	 	 	   I s S t a r t e d   & &                                                               / /   a n d   t h e   g a m e   i s   s t a r t e d  
 	 	 	 	   _ t o p C a r d s   [ 0 ]   ! =   n u l l   & &   _ t o p C a r d s   [ 0 ] . R a n k   = =   _ t o p C a r d s   [ 1 ] . R a n k )   / /   a n d   i t s   a   m a t c h  
 	 	 	 {  
 	 	 	 	 _ s c o r e   [ p l a y e r ] + + ;  
 	 	 	 	 / / T O D O :   c o n s i d e r   p l a y i n g   a   s o u n d   h e r e . . .  
 	 	 	 }    
 	 	 	 e l s e   i f   ( p l a y e r   > =   0   & &   p l a y e r   <   _ s c o r e . L e n g t h )    
 	 	 	 {  
 	 	 	 	 _ s c o r e   [ p l a y e r ] - - ;  
 	 	 	 }  
  
 	 	 	 / /   s t o p   t h e   g a m e . . .  
 	 	 	 _ s t a r t e d   =   f a l s e ;  
 	 	 	 _ g a m e T i m e r . S t o p   ( ) ;  
 	 	 }  
  
 	 	 # r e g i o n   S n a p   G a m e   U n i t   T e s t s  
 # i f   D E B U G  
  
 	 	 p u b l i c   c l a s s   S n a p T e s t s  
 	 	 {  
 	 	 	 [ T e s t ]  
 	 	 	 p u b l i c   v o i d   T e s t S n a p C r e a t i o n ( )  
 	 	 	 {  
 	 	 	 	 S n a p   s   =   n e w   S n a p ( ) ;  
  
 	 	 	 	 A s s e r t . I s T r u e ( s . C a r d s R e m a i n ) ;  
 	 	 	 	 A s s e r t . I s N u l l   ( s . T o p C a r d ) ;  
 	 	 	 }  
  
 	 	 	 [ T e s t ]  
 	 	 	 p u b l i c   v o i d   T e s t F l i p N e x t C a r d ( )  
 	 	 	 {  
 	 	 	 	 S n a p   s   =   n e w   S n a p ( ) ;  
  
 	 	 	 	 A s s e r t . I s T r u e ( s . C a r d s R e m a i n ) ;  
 	 	 	 	 A s s e r t . I s N u l l   ( s . T o p C a r d ) ;  
  
 	 	 	 	 s . F l i p N e x t C a r d   ( ) ;  
  
 	 	 	 	 A s s e r t . I s N u l l   ( s . _ t o p C a r d s   [ 0 ] ) ;  
 	 	 	 	 A s s e r t . I s N o t N u l l   ( s . _ t o p C a r d s   [ 1 ] ) ;  
 	 	 	 }  
 	 	 }  
  
 # e n d i f  
 	 	 # e n d r e g i o n  
 	 }  
 }  
  
 